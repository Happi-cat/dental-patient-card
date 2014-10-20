﻿'use strict';

angular.module('patient-card.directives')
	.directive('myTableHead', function() {
		return {
			restrict: 'A',
			scope: {
				tableHead: '=myTableHead',
				sortFields: '=mySortFields'
			},
			templateUrl: '/Widget/TableHeader',
			controller: function ($scope) {
				$scope.toogleSort = function (item) {
					if (item.sort == 1) {
						item.sort = -1;
					} else if (item.sort == -1) {
						item.sort = 0;
					} else {
						item.sort = 1;
					}

					if (!$scope.sortFields || !$scope.tableHead)
						return;

					$scope.sortFields.splice(0, $scope.sortFields.length);
					angular.forEach($scope.tableHead, function (column) {
						if (column.sort == 1) {
							$scope.sortFields.push(column.name);
						} else if (column.sort == -1) {
							$scope.sortFields.push('-' + column.name);
						}
					});

				};
			}
		};
	});