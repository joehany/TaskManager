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
        createNewItem: function (newItem) {
            var deferred = $q.defer();
            var url = window.config.serverUrl + '/api/task?name=' + newItem.name + "&listId=" + newItem.listId;
            $http.post(url).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        updateItem: function () {
            var deferred = $q.defer();
            $http.post(window.config.serverUrl + '/api/list').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        deleteItem: function () {
            var deferred = $q.defer();
            $http.post(window.config.serverUrl + '/api/list').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
    };
})