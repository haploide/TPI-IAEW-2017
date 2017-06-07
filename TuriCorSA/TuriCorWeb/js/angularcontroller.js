var module = angular.module('app', ['ngRoute'])
    .config(function($routeProvider){
        $routeProvider.when('/listarvehiculos', {
            controller: 'vehiculosController as vehiculosList',
            templateUrl: 'templates/VehiculosView.html'

        })
    }).controller('vehiculosController', function($http){
        var vehiculosController = this;
        vehiculosController.title = 'Consulta de Vehiculos Disponibles';
        vehiculosController.vehiculo = [];
        vehiculosController.isBusy = true;
        $http({
            method: 'GET',
            url: 'http://localhost:2253/api/vehiculo?Id=2&fechaHoraRetiro=2016-1-1T00:00:00&fechaHoraDevolucion=2016-3-27T23:59:59',
            headers: {
                'Accept': "application/json"
            }
        }).then(function (response) {
            angular.copy(response.data, vehiculosController.vehiculo);
            vehiculosController.isBusy = false;

        }, function (response) {
            alert("Error");
        }).then(function () {

        });
    })