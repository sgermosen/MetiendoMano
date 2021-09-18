<?php
namespace App\Core\DB;
abstract class ORM
{
    protected static
        $conn,
        $database,
        $pk = 'id';

    private
        $reflectionObject,
        $loadMethod,
        $loadData,
        $modifiedFields = array(),
        $lastRecord,
        $isNew = false;


    protected
        $parentObject,
        $ignoreKeyOnUpdate = true,
        $ignoreKeyOnInsert = true;

    /**
     * ER Fine Tuning
     */
    const
        FILTER_IN_PREFIX = 'filterIn',
        FILTER_OUT_PREFIX = 'filterOut';

    /**
     * Loading options.
     */
    const
        LOAD_BY_PK = 1,
        LOAD_BY_ARRAY = 2,
        LOAD_NEW = 3,
        LOAD_EMPTY = 4;

    /**
     * Constructor.
     *
     * @access public
     * @param mixed $data
     * @param integer $method
     */
    final public function __construct ($data = null, $method = self::LOAD_EMPTY)
    {
        // store raw data
        $this->loadData = $data;
        $this->loadMethod = $method;

        // load our data
        switch ($method)
        {
            case self::LOAD_BY_PK:
                $this->loadByPK();
                break;

            case self::LOAD_BY_ARRAY:
                $this->loadByArray();
                break;

            case self::LOAD_NEW:
                $this->loadByArray();
                $this->insert();
                break;

            case self::LOAD_EMPTY:
                $this->hydrateEmpty();
                break;
        }

        $this->initialise();
    }


    public static function useConnection (\PDO $conn)
    {
        self::$conn = $conn;

    }

    /**
     * Get our connection instance.
     *
     * @access public
     * @static
     * @return mysqli
     */
    public static function getConnection ()
    {
        return self::$conn;
    }

    /**
     * Get load method.
     *
     * @access public
     * @return integer
     */
    public function getLoadMethod ()
    {
        return $this->loadMethod;
    }

    /**
     * Get load data (raw).
     *
     * @access public
     * @return array
     */
    public function getLoadData ()
    {
        return $this->loadData;
    }

    /**
     * Load ER by Primary Key
     *
     * @access private
     * @return void
     */
    private function loadByPK ()
    {
        // populate PK
        $this->{self::getTablePk()} = $this->loadData;

        // load data
        $this->hydrateFromDatabase();
    }

    /**
     * Load ER by array hydration.
     *
     * @access private
     * @return void
     */
    private function loadByArray ()
    {
        // set our data
        foreach ($this->loadData AS $key => $value)
            $this->{$key} = $value;

        // extract columns
        $this->executeOutputFilters();
    }

    /**
     * Hydrate the object with null values.
     * Fetches column names using DESCRIBE.
     *
     * @access private
     * @return void
     */
    private function hydrateEmpty ()
    {
        // set our data
        if (isset($this->erLoadData) && is_array($this->erLoadData))
            foreach ($this->erLoadData AS $key => $value)
                $this->{$key} = $value;

        foreach ($this->getColumnNames() AS $field)
            $this->{$field} = null;

        // mark object as new
        $this->isNew = true;
    }

    /**
     * Fetch the data from the database.
     *
     * @access private
     * @throws \Exception If the record is not found.
     * @return void
     */
    private function hydrateFromDatabase ()
    {
        $sql = sprintf("SELECT * FROM %s WHERE %s = '%s';", self::getTableName(), self::getTablePk(), $this->id());
        $result = self::getConnection()->query($sql);
        if (!$result->rowCount())
            throw new \Exception(sprintf("%s record not found in database. (PK: %s)", get_called_class(), $this->id()), 2);
        foreach ($result->fetch(\PDO::FETCH_ASSOC) AS $key => $value)
            $this->{$key} = $value;

//        self::getConnection()->close();

        // extract columns
        $this->executeOutputFilters();
    }

    /**
     * Get the database name for this ER class.
     *
     * @access public
     * @static
     * @return string
     */
    public static function getDatabaseName ()
    {
        $className = get_called_class();

        return $className::$database;
    }

    /**
     * Get the table name for this ER class.
     *
     * @access public
     * @static
     * @return string
     */
    public static function getTableName ()
    {
        $className = get_called_class();

        // static prop config
        if (isset($className::$table))
            return $className::$table;

        // assumed config
        return strtolower($className);
    }

    /**
     * Get the PK field name for this ER class.
     *
     * @access public
     * @static
     * @return string
     */
    public static function getTablePk ()
    {
        $className = get_called_class();

        return $className::$pk;
    }

    /**
     * Return the PK for this record.
     *
     * @access public
     * @return integer
     */
    public function id ()
    {
        return $this->{self::getTablePk()};
    }

    /**
     * Check if the current record has just been created in this instance.
     *
     * @access public
     * @return boolean
     */
    public function isNew ()
    {
        return $this->isNew;
    }

    /**
     * Executed just before any new records are created.
     * Place holder for sub-classes.
     *
     * @access public
     * @return void
     */
    public function preInsert ()
    {
    }

    /**
     * Executed just after any new records are created.
     * Place holder for sub-classes.
     *
     * @access public
     * @return void
     */
    public function postInsert ()
    {
    }

    /**
     * Executed just after the record has loaded.
     * Place holder for sub-classes.
     *
     * @access public
     * @return void
     */
    public function initialise ()
    {
    }

    /**
     * Execute these filters when loading data from the database.
     *
     * @access private
     * @return void
     */
    private function executeOutputFilters ()
    {
        $r = new \ReflectionClass(get_class($this));

        foreach ($r->getMethods() AS $method)
            if (substr($method->name, 0, strlen(self::FILTER_OUT_PREFIX)) == self::FILTER_OUT_PREFIX)
                $this->{$method->name}();
    }

    /**
     * Execute these filters when saving data to the database.
     *
     * @access private
     * @return void
     */
    private function executeInputFilters ($array)
    {
        $r = new \ReflectionClass(get_class($this));

        foreach ($r->getMethods() AS $method)
            if (substr($method->name, 0, strlen(self::FILTER_IN_PREFIX)) == self::FILTER_IN_PREFIX)
                $array = $this->{$method->name}($array);

        return $array;
    }

    /**
     * Save (insert/update) to the database.
     *
     * @access public
     * @return void
     */
    public function save ()
    {
        if ($this->isNew())
            $this->insert();
        else
            $this->update();
    }

    /**
     * Insert the record.
     *
     * @access private
     * @throws \Exception
     * @return void
     */
    private function insert ()
    {
        $array = $this->get();

        // run pre inserts
        $this->preInsert($array);

        // input filters
        $array = $this->executeInputFilters($array);

        // remove data not relevant
        $array = array_intersect_key($array, array_flip($this->getColumnNames()));

        // to PK or not to PK
        if ($this->ignoreKeyOnInsert === true)
            unset($array[self::getTablePk()]);

        // compile statement
        $fieldNames = $fieldMarkers = $values = array();

        foreach ($array AS $key => $value)
        {
            $fieldNames[] = sprintf('%s', $key);
            $fieldMarkers[] = ":$key";
            $values[] = &$array[$key];
        }

        // build sql statement
        $sql = sprintf("INSERT INTO %s (%s) VALUES (%s)", self::getTableName(), implode(', ', $fieldNames), implode(',', $fieldMarkers));
        $params=array_combine($fieldMarkers,$values);

        // prepare, bind & execute
        try{
            $stmt = self::getConnection()->prepare($sql);
            $stmt->execute($params);

            // set our PK (if exists)
            $this->lastRecord = self::getConnection()->lastInsertId();
            if (self::getConnection()->lastInsertId())
                $this->{self::getTablePk()} = self::getConnection()->lastInsertId();

            // mark as old
            $this->isNew = false;

            // hydrate
            $this->hydrateFromDatabase(self::getConnection()->lastInsertId());

            // run post inserts
            $this->postInsert();
        }catch (\Exception $e){
            $e->getMessage();

            var_dump($e->getMessage());
        }


    }

    /**
     * Update the record.
     *
     * @access public
     * @throws \Exception
     * @return void
     */
    public function update ()
    {
        if ($this->isNew())
            throw new \Exception('Unable to update object, record is new.');

        $pk = self::getTablePk();
        $id = $this->id();

        // input filters
        $array = $this->executeInputFilters($this->get());

        // remove data not relevant
        $array = array_intersect_key($array, array_flip($this->getColumnNames()));

        // to PK or not to PK
        if ($this->ignoreKeyOnUpdate === true)
            unset($array[$pk]);

        // compile statement
        $fields = $values = $params=array();

        foreach ($array AS $key => $value)
        {
            $fields[] = sprintf("%s = :$key", $key);
//            $values[] = &$array[$key];
            $params[":$key"]=&$array[$key];
        }

        // where
        $params[":$pk"] = &$id;

        // build sql statement
        $sql = sprintf("UPDATE %s SET %s WHERE %s = :$pk",self::getTableName(), implode(', ', $fields), $pk);
        // prepare, bind & execute
        try{
            $stmt = self::getConnection()->prepare($sql);
            $stmt->execute($params);
        }catch (\Exception $e){
//            dispalyForDebug($e);die();
            $e->getMessage();
        }


        // reset modified list
        $this->modifiedFields = array();
    }

    /**
     * Delete the record from the database.
     *
     * @access public
     * @return void
     * @throws \Exception
     */
    public function delete ()
    {
        if ($this->isNew())
            throw new \Exception('Unable to delete object, record is new (and therefore doesn\'t exist in the database).');

        // build sql statement
        $sql = sprintf("DELETE FROM %s WHERE %s = :id",self::getTableName(), self::getTablePk());
        try{
            // prepare, bind & execute
            $stmt = self::getConnection()->prepare($sql);
            $id = $this->id();
            $stmt->bindParam(":id", $id);
            $stmt->execute();
        }catch (\Exception $e){
            $e->getMessage();
        }

    }

    /**
     * Fetch column names directly from MySQL.
     *
     * @access public
     * @return array
     * @throws Exception
     */
    public function getColumnNames ()
    {
        try {


            $conn = self::getConnection();
            $result = $conn->query(sprintf("DESCRIBE %s;", self::getTableName()));

            if ($result === false)
                throw new \Exception(sprintf('Unable to fetch the column names. %s.'));

            $ret = array();

            while ($row = $result->fetch(\PDO::FETCH_ASSOC))
                $ret[] = $row['Field'];

// close connection
        }catch (\Exception $e){
            $e->getMessage();
        }
        return $ret;
    }


    /**
     * Get/set the parent object for this record.
     * Useful if you want to access the owning record without looking it up again.
     *
     * Use without parameters to return the parent object.
     *
     * @access public
     * @param bool|object $obj
     * @return object
     */
    public function parent ($obj = false)
    {
        if ($obj && is_object($obj))
            $this->parentObject = $obj;

        return $this->parentObject;
    }

    /**
     * Revert the object by reloading our data.
     *
     * @access public
     * @param boolean $return If true the current object won't be reverted, it will return a new object via cloning.
     * @return void | clone
     */
    public function revert ($return = false)
    {
        if ($return)
        {
            $ret = clone $this;
            $ret->revert();

            return $ret;
        }

        $this->hydrateFromDatabase();
    }

    /**
     * Get a value for a particular field or all values.
     *
     * @access public
     * @param bool|string $fieldName If false (default), the entire record will be returned as an array.
     * @return array|string
     */
    public function get ($fieldName = false)
    {
        // return all data
        if ($fieldName === false)
            return self::convertObjectToArray($this);

        return $this->{$fieldName};
    }

    /**
     * Convert an object to an array.
     *
     * @access public
     * @static
     * @param object $object
     * @return array
     */
    public static function convertObjectToArray ($object)
    {
        if (!is_object($object))
            return $object;

        $array = array();
        $r = new \ReflectionObject($object);

        foreach ($r->getProperties(\ReflectionProperty::IS_PUBLIC) AS $key => $value)
        {
            $key = $value->getName();
            $value = $value->getValue($object);

            $array[$key] = is_object($value) ? self::convertObjectToArray($value) : $value;
        }

        return $array;
    }

    /**
     * Set a new value for a particular field.
     *
     * @access public
     * @param string $fieldName
     * @param string $newValue
     * @return void
     */
    public function set ($fieldName, $newValue)
    {
        // if changed, mark object as modified
        if ($this->{$fieldName} != $newValue)
            $this->modifiedFields($fieldName, $newValue);

        $this->{$fieldName} = $newValue;

        return $this;
    }

    /**
     * Check if our record has been modified since boot up.
     * This is only available if you use set() to change the object.
     *
     * @access public
     * @return array | false
     */
    public function isModified ()
    {
        return (count($this->modifiedFields) > 0) ? $this->modifiedFields : false;
    }

    /**
     * Mark a field as modified & add the change to our history.
     *
     * @access private
     * @param string $fieldName
     * @param string $newValue
     * @return void
     */
    private function modifiedFields ($fieldName, $newValue)
    {
        // add modified field to a list
        if (!isset($this->modifiedFields[$fieldName]))
        {
            $this->modifiedFields[$fieldName] = $newValue;

            return;
        }

        // already modified, initiate a numerical array
        if (!is_array($this->modifiedFields[$fieldName]))
            $this->modifiedFields[$fieldName] = array($this->modifiedFields[$fieldName]);

        // add new change to array
        $this->modifiedFields[$fieldName][] = $newValue;
    }

    /**
     * Fetch & return one record only.
     */
    const FETCH_ONE = 1;

    /**
     * Fetch multiple records.
     */
    const FETCH_MANY = 2;

    /**
     * Don't fetch.
     */
    const FETCH_NONE = 3;

    /**
     * Execute an SQL statement & get all records as hydrated objects.
     *
     * @access public
     * @param string $sql
     * @param integer $return
     * @return mixed
     * @throws Exception
     */
    public static function sql ($sql, $return = ORM::FETCH_MANY)
    {
        // shortcuts
        $sql = str_replace(array(':table', ':pk'), array(self::getTableName(), self::getTablePk()), $sql);

        // execute
        $result = self::getConnection()->query($sql);

        if (!$result)
            throw new \Exception(sprintf('Unable to execute SQL statement. %s', self::getConnection()->errorInfo()));

        if ($return === ORM::FETCH_NONE)
            return;

        $ret = array();

        while ($row = $result->fetch(\PDO::FETCH_ASSOC))
            $ret[] = call_user_func_array(array(get_called_class(), 'hydrate'), array($row));

//        $result->close();

        // return one if requested
        if ($return === ORM::FETCH_ONE)
            $ret = isset($ret[0]) ? $ret[0] : null;

        return $ret;
    }

    /**
     * Execute a Count SQL statement & return the number.
     *
     * @access public
     * @param string $sql
     * @return mixed
     * @internal param int $return
     */
    public static function count ($sql)
    {
        $count = self::sql($sql, ORM::FETCH_ONE);

        return $count > 0 ? $count : 0;
    }

    /**
     * Truncate the table.
     * All data will be removed permanently.
     *
     * @access public
     * @static
     * @return void
     */
    public static function truncate ()
    {
        self::sql('TRUNCATE :table', ORM::FETCH_NONE);
    }

    /**
     * Get all records.
     *
     * @access public
     * @return array
     */
    public static function all ()
    {
        return self::sql("SELECT * FROM :table");
    }

    /**
     * Retrieve a record by its primary key (PK).
     *
     * @access public
     * @param integer $pk
     * @return object
     */
    public static function retrieveByPK ($pk)
    {
        if (!is_numeric($pk))
            throw new \InvalidArgumentException('The PK must be an integer.');

        $reflectionObj = new \ReflectionClass(get_called_class());

        return $reflectionObj->newInstanceArgs(array($pk, ORM::LOAD_BY_PK));
    }

    /**
     * Load an ER object by array.
     * This skips reloading the data from the database.
     *
     * @access public
     * @param array $data
     * @return object
     */
    public static function hydrate ($data)
    {
        if (!is_array($data))
            throw new \InvalidArgumentException('The data given must be an array.');

        $reflectionObj = new \ReflectionClass(get_called_class());

        return $reflectionObj->newInstanceArgs(array($data, ORM::LOAD_BY_ARRAY));
    }

    /**
     * Retrieve a record by a particular column name using the retrieveBy prefix.
     * e.g.
     * 1) Foo::retrieveByTitle('Hello World') is equal to Foo::retrieveByField('title', 'Hello World');
     * 2) Foo::retrieveByIsPublic(true) is equal to Foo::retrieveByField('is_public', true);
     *
     */
    public static function __callStatic ($name, $args)
    {
        $class = get_called_class();

        if (substr($name, 0, 10) == 'retrieveBy')
        {
            // prepend field name to args
            $field = strtolower(preg_replace('/\B([A-Z])/', '_${1}', substr($name, 10)));
            array_unshift($args, $field);

            return call_user_func_array(array($class, 'retrieveByField'), $args);
        }

        throw new \Exception(sprintf('There is no static method named "%s" in the class "%s".', $name, $class));
    }

    /**
     * Retrieve a record by a particular column name.
     *
     * @access public
     * @static
     * @param string $field
     * @param mixed $value
     * @param integer $return
     * @return mixed
     */
    public static function retrieveByField ($field, $value, $return = ORM::FETCH_MANY)
    {
        if (!is_string($field))
            throw new \InvalidArgumentException('The field name must be a string.');

        // build our query
        $operator = (strpos($value, '%') === false) ? '=' : 'LIKE';

        $sql = sprintf("SELECT * FROM :table WHERE %s %s '%s'", $field, $operator, $value);

        if ($return === ORM::FETCH_ONE)
            $sql .= ' LIMIT 0,1';

        // fetch our records
        return self::sql($sql, $return);
    }

    /**
     * Get array for select box.
     *
     * NOTE: Class must have __toString defined.
     *
     * @access public
     * @param string $where
     * @return array
     */
    public static function buildSelectBoxValues ($where = null)
    {
        $sql = 'SELECT * FROM :table';

        // custom where?
        if (is_string($where))
            $sql .= sprintf(" WHERE %s", $where);

        $values = array();

        foreach (self::sql($sql) AS $object)
            $values[$object->id()] = (string) $object;

        return $values;
    }



    public  function  getLastInserted()
    {
        return $this->lastRecord;
    }
}
