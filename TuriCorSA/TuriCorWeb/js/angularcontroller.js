var module = angular.module('app', ['ngRoute'])
    .config(function($routeProvider){
        $routeProvider.when("/", {
            controller: "homeController as home",
            templateUrl: "/templates/homeView.html"
        }).when("/listarvehiculos", {
                controller: "vehiculosController as vehiculosList",
                templateUrl: "templates/VehiculosView.html"

        }).when("/listarreservas", {
            controller: "reservasController as reservasList",
            templateUrl: "templates/ReservasView.html"

        }).when("/nuevareserva", {
            controller: "nuevareservasController as registrarReserva",
            templateUrl: "templates/registrarReservaView.html"

        }).otherwise({
            redirectTo: "/"
        });
    }).factory('reserva', function () {
        return { FechaReserva: moment().format('YYYY-MM-DD') };


    }).controller('homeController', function ($http) {
        var homeController = this;
        homeController.title = "TuriCor S.A.";

    }).controller('vehiculosController', function ($http, $scope, $location, reserva) {
        var vehiculosController = this;
        vehiculosController.title = 'Consulta de Vehiculos Disponibles';
        vehiculosController.vehiculos = [];
        vehiculosController.paises = [];
        vehiculosController.ciudades = [];
        vehiculosController.nuevareserva = function (vehi) {
            reserva.FechaHoraRetiro = $scope.fechaDesde;
            reserva.FechaHoraDevolucion = $scope.fechaHasta;
            reserva.IdVehiculoCiudad = vehi.VehiculoCiudadId;
            reserva.IdCiudad = vehiculosController.ciudadSeleccionada;
            reserva.Costo = (vehi.PrecioPorDia*100)/120;
            //reserva.IdPais=
            reserva.PrecioVenta = vehi.PrecioPorDia;

            $location.url('/nuevareserva');
            
        }
        vehiculosController.seleccionarCiudad=function (id) {
            vehiculosController.ciudadSeleccionada = id;
        };
        
        vehiculosController.buscarVehiculos = function () {
            vehiculosController.isBusy = true;
            $http({
                method: 'GET',
                url: 'http://localhost:2253/api/vehiculo?Id=' + vehiculosController.ciudadSeleccionada + '&fechaHoraRetiro=' + $scope.fechaDesde + '&fechaHoraDevolucion=' + $scope.fechaHasta,
                headers: {
                    'Accept': "application/json"
                }
            }).then(function (response) {
                angular.copy(response.data.VehiculosEncontrados, vehiculosController.vehiculos);
                $.each(vehiculosController.vehiculos, function () {
                    if (this.TieneAireAcon) {
                        this.TieneAireAcon = 'Si';
                    } else {
                        this.TieneAireAcon = 'No';
                    }
                    if (this.TieneDireccion) {
                        this.TieneDireccion = 'Si';
                    } else {
                        this.TieneDireccion = 'No';
                    }
                    this.CiudadId = $('#cmbCiudad option:selected').text();
                });
                
                vehiculosController.isBusy = false;

            }, function (response) {
                alert("Error");
            }).then(function () {

            });
            $('.clickable-row').click(function () {
                alert('funca');
            })
        }
        vehiculosController.buscarCiudades = function (pais) {
            //alert(pais);
            vehiculosController.isBusy = true;
            $http({
                method: 'GET',
                url: 'http://localhost:2253/api/ciudad?id='+pais.Id,
                headers: {
                    'Accept': "application/json"
                }
            }).then(function (response) {
                angular.copy(response.data.Ciudades, vehiculosController.ciudades);
                vehiculosController.isBusy = false;

            }, function (response) {
                alert("Error");
            }).then(function () {

            });
            $('#cmbCiudad').removeAttr('disabled');
        }
        vehiculosController.isBusy = true;
        $http({
            method: 'GET',
            url: 'http://localhost:2253/api/pais',
            headers: {
                'Accept': "application/json"
            }
        }).then(function (response) {
            angular.copy(response.data.Paises, vehiculosController.paises);
            vehiculosController.isBusy = false;

        }, function (response) {
            alert("Error");
        }).then(function () {

        });
        vehiculosController.limpiar = function () {
            $('#cmbCiudad').attr('disabled', true);
            $('#datepickerHasta').val('');
            $('#datepickerDesde').val('');
            $('#cmbCiudad option:eq(0)').prop('selected', true);
            $('#cmbPais option:eq(0)').prop('selected', true);
            vehiculosController.vehiculos = [];
            vehiculosController.ciudades = [];
            $scope.fechaDesde = '';
            $scope.fechaHasta = '';
            vehiculosController.ciudadSeleccionada = '';

        }
       

    }).controller('reservasController', function ($http, $scope) {
        var reservasController = this;
        reservasController.title = 'Consulta de reservas realizadas';
        reservasController.reservas = [];
        reservasController.paises = [];
        reservasController.ciudad = [];
        //CANCELAR RESERVA
        reservasController.delete = function (res) {
          //  alert(res.Vendedor.Nombre)
            $http({
                method: 'DELETE',
                
                url: 'http://localhost:2253/api/reserva/'+res.Id,
               
                headers: {
                    'Accept': "application/json"
                }
            }).then(function (response) {
                // $window.location = "#/clientes";
                alert('Exito');
            }, function (response) {
                alert("Error");
            }).then(function () {

            });
        };
        reservasController.buscarReservas = function () {
            reservasController.isBusy = true;
            $http({
                method: 'GET',
                url: 'http://localhost:2253/api/Reserva',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': "application/json"
                }
            }).then(function (response) {
                angular.copy(response.data, reservasController.reservas);
                
                $http({
                    method: 'GET',
         
                 url: 'http://localhost:2253/api/pais?id='+ reservasController.reservas[0].IdPais,
                    headers: {
                      'Accept': "application/json"
                }
                }).then(function (response) {
                    angular.copy(response.data, reservasController.paises);
                    $.each(reservasController.reservas, function () {

                        this.IdPais = reservasController.paises.Nombre;

                    });
                }, function (response) {
                    alert("Error");
                }).then(function () {

                });
                $http({
                    method: 'GET',

                    url: 'http://localhost:2253/api/ciudad?idCiudad=' + reservasController.reservas[0].IdCiudad + '&idPais=' + reservasController.reservas[0].IdPais,
                    headers: {
                        'Accept': "application/json"
                    }
                }).then(function (response) {
                    angular.copy(response.data, reservasController.ciudad);
                    $.each(reservasController.reservas, function () {

                        this.IdCiudad = reservasController.ciudad.Nombre;

                    });
                }, function (response) {
                    alert("Error");
                }).then(function () {

                });
                
                reservasController.isBusy = false;

            }, function (response) {
                alert("Error");
            }).then(function () {

            });
        }

    }).controller('nuevareservasController', function ($http, $scope, reserva) {
        var nuevareservasController = this;
        nuevareservasController.title = 'Registrar Nueva Reserva';
        nuevareservasController.LugaresRetiroDevolucion = [{ 'Id': 0, 'Nombre': 'Aeropuerto' }, { 'Id': 1, 'Nombre': 'Terminal de Buses' }, { 'id': 2, 'Nombre': 'Hotel' }]
        nuevareservasController.nuevaReserva = reserva;
        nuevareservasController.selectRetiro = function (nombre) {
            reserva.LugarRetiro = nombre;
        };
        nuevareservasController.selectDevolucion = function (nombre) {
            reserva.LugarDevolucion = nombre;
        };
        nuevareservasController.save = function () {
            $http({
                method: 'POST',
                url: 'http://localhost:2253/api/reserva',
                data: nuevareservasController.nuevaReserva,
                headers: {
                    'Accept': "application/json"
                }
            }).then(function (response) {
               // $window.location = "#/clientes";
                alert('Exito');
            }, function (response) {
                alert("Error");
            }).then(function () {

            });
        };

    });




