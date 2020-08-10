var repo = function () {

    var db = {};

    var get = function (id) {
        console.log('Getting project ' + id);
        return {
            name: 'New project'
        }
    }

    var save = function (project) {
        console.log('Saving ' + project.name + ' to the db');
    }
    
    
    return {
        get: get,
        save: save
    }


}

module.exports = repo;