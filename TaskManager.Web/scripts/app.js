var taskManager = angular.module("taskManager", ['ngRoute','ngResource', 'xeditable']);
taskManager.config(function ($routeProvider) {
        $routeProvider
            .when('/list/:id',
                {
                    templateUrl: '/templates/list.html',
                    controller: 'listController'
                })
            .when('/',
                {
                    templateUrl: '/templates/home.html',
                    controller: 'homeController'
                });

}).run(function (editableOptions) {
    editableOptions.theme = 'bs3'; // bootstrap3 theme. Can be also 'bs2', 'default'
});;