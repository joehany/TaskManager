taskManager.factory('listRepository', function ($http, $q, $rootScope) {
    $rootScope.loading = false;
    $http.defaults.transformRequest.push(function (data) {
        $rootScope.$broadcast('httpCallStarted');
        return data;
    });
    $http.defaults.transformResponse.push(function (data) {
        $rootScope.$broadcast('httpCallStopped');
        return data;
    });
    
    $rootScope.$on('httpCallStarted', function (e) {
        $rootScope.loading = true;

    });

    $rootScope.$on('httpCallStopped', function (e) {
        $rootScope.loading = false;

    });
    return {
        getListById: function (id) {
            var deferred = $q.defer();
            $http.get(window.config.serverUrl + '/api/list/'+id).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;;
        },
        createNewList: function() {
            var deferred = $q.defer();
            $http.post(window.config.serverUrl + '/api/list').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        createNewTask: function (newTask) {
            var deferred = $q.defer();
            var url = window.config.serverUrl + '/api/task';
            $http.post(url, newTask).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        updateTask: function (task) {
            var deferred = $q.defer();
            var url = window.config.serverUrl + '/api/task/' + task.Id ;
            $http.put(url, task).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        deleteTask: function (id) {
            var deferred = $q.defer();
            var url = window.config.serverUrl + '/api/task/' + id;
            $http.delete(url).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
    };
})