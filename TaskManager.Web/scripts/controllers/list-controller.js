taskManager.controller("listController", function ($scope, listRepository, $routeParams, $location) {
    $scope.gettingList = true;
    var id = $routeParams.id;
    if (id == undefined) {
        //todo:error
    } else {
        listRepository.getListById(id).then(function (list) {
            $scope.gettingList = false;
            if (list != "null") {
                $scope.list = list;
                localStorage.setItem("list", list.Id);

            } else {
                $scope.error = true;
                $scope.errorTitle = "Ooops!";
                $scope.errorMessage = "We didn't find a list with such id: " +id + "....  Please hit \"New List\" button to create a new list";
            }

        },
            function (error) {
                $scope.error = true;
                $scope.errorTitle = "Server Error!";
                $scope.errorMessage = error.Message;
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
                $scope.error = true;
                $scope.errorTitle = "Server Error!";
                $scope.errorMessage = error;
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
                $scope.error = true;
                $scope.errorTitle = "Server Error!";
                $scope.errorMessage = error;
            });
        }

    };
    $scope.deleteTask = function (task) {
        listRepository.deleteItem(task.Id).then(function () {
            var index = $scope.list.Items.indexOf(task);
            if (index > -1) {
                $scope.list.DateModified = new Date();
                $scope.list.Items.splice(index, 1);
            }
        },
            function (error) {
                $scope.error = true;
                $scope.errorTitle = "Server Error!";
                $scope.errorMessage = error;
            });
    };
    $scope.changeTaskStatus = function (task) {
        listRepository.updateItem(task).then(function () {
            $scope.list.DateModified = new Date();
        },
            function (error) {
                $scope.error = true;
                $scope.errorTitle = "Server Error!";
                $scope.errorMessage = error.value;
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