taskManager.controller("listController", function ($scope, listRepository, $routeParams, $location) {
    $scope.gettingList = true;
    var id = $routeParams.id;
    if (id == undefined) {
        //todo:error
    } else {
        listRepository.getListById(id).then(function (list) {
            $scope.list = list;
            localStorage.setItem("list", list.Id);
            $scope.gettingList = false;

        },
            function (error) {
                $scope.serverError = true;
                $scope.serverErrormessage = error.Message;
            });
    }
    $scope.saveItem = function () {
        $scope.newItem.listId = $scope.list.Id;
        listRepository.createNewItem($scope.newItem).then(
            function (createdItem) {
                $scope.list.Items.push(createdItem);
                $scope.list.DateModified = createdItem.DateModified;
            },
            function (error) {
                $scope.serverError = true;
                $scope.serverErrormessage = error;
            });

    };
    $scope.createNewList = function () {
        listRepository.createNewList().then(function (list) {
            $location.path('/list/' + list.Id);
        });
    };
    $scope.updateTask = function (name, task) {
        if (name == "")
            return "Please enter task name";
        if (name != task.Name) {
            task.Name = name;
            listRepository.updateItem(task).then(function () {
                $scope.list.DateModified = new Date();
            },
            function (error) {
                $scope.serverError = true;
                $scope.serverErrormessage = error;
            });
        }

    };
    $scope.deleteTask = function (task) {
        listRepository.deleteItem(task.Id).then(function () {
            var index = $scope.list.Items.indexOf(task);
            if (index > -1) {
                $scope.list.Items.splice(index, 1);
            }
        },
            function (error) {
                $scope.serverError = true;
                $scope.serverErrormessage = error;
            });
    };
    $scope.changeTaskStatus = function (task) {
        listRepository.updateItem(task).then(function () {
            $scope.list.DateModified = new Date();
        },
            function (error) {
                $scope.serverError = true;
                $scope.serverErrormessage = error.value;
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
        catch (ex) {
            return 0;
        }
    };
});