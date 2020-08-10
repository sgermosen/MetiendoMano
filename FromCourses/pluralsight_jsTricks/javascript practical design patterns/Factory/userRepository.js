var repo = function () {

    var db = {};

    var get = function (id) {
        console.log('Getting user ' + id);
        return {
            name: 'Jon Mills'
        }
    }

    var save = function (user) {
        console.log('Saving ' + user.name + ' to the db');
    }
    
    
    return {
        get: get,
        save: save
    }


}

module.exports = repo;