taskManager.controller("listController", function($scope, listRepository, $routeParams,$location) {
    var id = $routeParams.id;
    if (id == undefined) {
        //todo:error
    } else {
        listRepository.getListById(id).then(function(list) {
             $scope.list = list;
        });
    }
    $scope.saveItem = function () {
        $scope.newItem.listId = $scope.list.Id;
        listRepository.createNewItem($scope.newItem).then(function(createdItem) {
            $scope.list.Items.push(createdItem);
            $scope.list.DateModified = createdItem.DateModified;
            $scope.newItem.name = "";
        });
       
    };
    $scope.createNewList = function () {
        listRepository.createNewList().then(function (list) {
            $location.path('/list/' + list.Id);
        });
    };
    $scope.updateTask = function (name,id) {
        var task = $scope.list.Items[0];
        task.Name = name;
        listRepository.updateItem(id, task).then(function (updateTask) {
            $scope.list.DateModified = updateTask.DateModified;
        });
    };
    $scope.changeTaskStatus = function (task) {
       
        listRepository.updateItem(task).then(function (updateTask) {
            $scope.list.DateModified = updateTask.DateModified;
        });
    };
    $scope.getPercentage = function () {
        try {
            var checked = 0;
            for (var i = 0; i < $scope.list.Items.length; i++) {
                if ($scope.list.Items[i].IsDone)
                    checked++;
            }
            return Math.floor((checked / $scope.list.Items.length) * 100);
        }
        catch (ex)
        {
            return 0;
        }
    };
});