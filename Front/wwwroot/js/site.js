var serverIp = '192.168.43.111';
var app = angular.module('mainApp', ['ngRoute']);
app.config(function($routeProvider,$httpProvider) {
    this.getHackList = function ($http, $q) {  
        var deferred = $q.defer();
        $http.get(`http://${serverIp}/api/hackatons`)  
        .then(function (response) {  
            deferred.resolve(response.data);
        });
        return deferred.promise;
    };
    this.getUserList = function ($http, $q) {  
                var deferred = $q.defer();
                $http.get(`http://${serverIp}/api/users`)  
                .then(function (response) {  
                    deferred.resolve(response.data);
                });
                return deferred.promise;
    };
    $routeProvider.when("/", {
        templateUrl : "main.html",
        controller : "mainCtrl",
        resolve: {  
            hackList: getHackList
        }
    }).when("/inserter", {
        templateUrl : "inserter.html",
        controller : "voidCtrl"
    }).when("/where2go", {
        templateUrl : "where2go.html",
        controller : "voidCtrl"
    }).when("/about", {
        templateUrl : "about.html",
        controller : "voidCtrl"
    }).when("/search", {
        templateUrl : "search.html",
        controller : "voidCtrl"
    }).when("/user", {
        templateUrl : "user.html",
        controller : "userCtrl",
        resolve: {
            userList: getUserList
        }
    }).when("/userreg", {
        templateUrl : "userreg.html",
        controller : "userRegCtrl"
    }).when("/teamreg", {
        templateUrl : "teamreg.html",
        controller : "teamRegCtrl",
        resolve: {  
            hackList: getHackList
        }
    }).when("/team", {
        templateUrl : "team.html",
        controller : "teamCtrl",
        resolve: {
            userList: getUserList,
            hackList: getHackList
        }
    }).when("/join", {
        templateUrl : "join.html",
        controller : "teamJoinCtrl",
        resolve: {
            userList: getUserList,
            hackList: getHackList
        }
    }).when("/scores", {
        templateUrl : "scores.html",
        controller : "addScoreCtrl",
        resolve: {
            hackList: getHackList
        }
    }).when("/edithack", {
        templateUrl : "edithack.html",
        controller : "editHackCtrl"
    }).when("/editcase", {
        templateUrl : "editcase.html",
        controller : "editCaseCtrl",
        resolve: {  
            hackList: getHackList
        }
    }).when("/editcheck", {
        templateUrl : "editcheck.html",
        controller : "editCheckCtrl",
        resolve: {  
            hackList: getHackList
        }
    }).when("/editcrit", {
        templateUrl : "editcrit.html",
        controller : "editCritCtrl",
        resolve: {  
            hackList: getHackList
        }
    }).otherwise({
        redirectTo: '/'
    });
});
app.controller("voidCtrl", function ($scope,$http) {});

app.controller("inserterCtrl", function ($scope,$http) {
    $scope.msg = "Ma-ma-main!";
    debugger;
    $scope.getTest = function() {
    var promiseResp = $http.get(`http://${serverIp}/api/test/asd`);
    promiseResp.then(function success(data,status,headers,config) {
      $scope.msg = data.data;
    },
    function error(data, status,headers, config) {
      $scope.msg = "Error";
    });
  }
});

app.controller("mainCtrl", function (hackList,$scope,$http) {
    var vm = this;
    $scope.hackList = hackList;
    vm.reloadData = function () {  
        $route.reload();  
    }
});

app.controller("userRegCtrl", function ($scope,$http) {
    $scope.userName = "";
    $scope.contacts = "";
    $scope.msg = "Статус";
    $scope.registerUser = function() {
    var promiseResp = $http.post(`http://${serverIp}/api/users`, {userName: $scope.userName, contacts: $scope.contacts});
    promiseResp.then(function success(data,status,headers,config) {
      $scope.msg = "Успешно";
    },
    function error(data, status,headers, config) {
      $scope.msg = "Ошибка";
    });
  }
});

app.controller("teamRegCtrl", function (hackList,$scope,$http) {
    var vm = this;
    $scope.hackList = hackList;
    $scope.hackatonId = "";
    $scope.teamName = "";
    $scope.msg = "Статус";
    vm.reloadData = function () {  
        $route.reload();  
    }
    $scope.registerTeam = function() {
    var promiseResp = $http.post(`http://${serverIp}/api/teams`, {name: $scope.teamName, hackatonId: $scope.hackatonId});
    promiseResp.then(function success(data,status,headers,config) {
      $scope.msg = "Успешно";
    },
    function error(data, status,headers, config) {
      $scope.msg = "Ошибка";
    });
  }
});

app.controller("teamJoinCtrl", function (userList,hackList,$scope,$http) {
    var vm = this;
    $scope.hackList = hackList;
    $scope.userList = userList;
    $scope.hackatonId = "";
    $scope.teamList = [];
    $scope.teamName = "";
    $scope.userId = "";
    $scope.teamId = "";
    $scope.msg = "Статус";
    vm.reloadData = function () {  
        $route.reload();  
    };
    $scope.joinTeam = function() {
      var promiseResp = $http.put(`http://${serverIp}/api/users/${$scope.userId}/joinTeam/${$scope.teamId}`,
       {userId: $scope.userId, teamId: $scope.teamId});
      promiseResp.then(function success(data,status,headers,config) {
        $scope.msg = "Успешно";
      },
      function error(data, status,headers, config) {
        $scope.msg = "Ошибка";
      });
    }
    $scope.onHackatonChanged=function() {
        var promiseResp = $http.get(`http://${serverIp}/api/hackatons/${$scope.hackatonId}/teams`);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.teamList = data.data;
        });
    }
});

app.controller("teamCtrl", function (userList,hackList,$scope,$http) {
    var vm = this;
    
    $scope.hackList = hackList;
    $scope.userList = userList;
    $scope.hackatonId = "";
    $scope.teamList = [];
    $scope.teamId = "";
    $scope.currentTeam = {id:-1};
    $scope.msg = "Статус";
    $scope.cases = [];
    $scope.checkpointsList = [];

    vm.reloadData = function () {  
        $route.reload();  
    };
    $scope.joinTeam = function() {
      var promiseResp = $http.put(`http://${serverIp}/api/users/${$scope.userId}/joinTeam/${$scope.teamId}`,
       {userId: $scope.userId, teamId: $scope.teamId});
      promiseResp.then(function success(data,status,headers,config) {
        $scope.msg = "Успешно";
      },
      function error(data, status,headers, config) {
        $scope.msg = "Ошибка";
      });
    }

    $scope.onTeamChanged=function() {
        if ($scope.teamId == null) return;
        var promiseResp = $http.get(`http://${serverIp}/api/teams/${$scope.teamId}`);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.currentTeam = data.data;
        });
    }

    $scope.getAllHackCases=function() {
        var promiseResp = $http.get(`http://${serverIp}/api/hackatons/${$scope.hackatonId}/cases`);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.cases = data.data;
        });
    }

    $scope.onHackatonChanged=function() {
        var promiseResp = $http.get(`http://${serverIp}/api/hackatons/${$scope.hackatonId}/teams`);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.teamList = data.data;
        });
        $scope.getAllHackCases();
        $scope.teamList.length = 0;
    }

    $scope.onCaseChanged=function() {
        var promiseResp = $http.get(`http://${serverIp}/api/cases/${$scope.currentTeam.case}/checkpoints`);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.checkpointsList = data.data;
        });
    }
    $scope.sendNewCase=function() {
        var promiseResp = $http.put(`http://${serverIp}/api/teams/${$scope.currentTeam.id}/setTeamCase/${$scope.currentTeam.case}`);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.msg = "Успешно";
        });
    }
});

app.controller("userCtrl", function (userList,$scope,$http) {
    var vm = this;
    $scope.userList = userList;
    $scope.user = {id:-1};
    $scope.msg = "Статус";
    vm.reloadData = function () {  
        $route.reload();  
    }
    $scope.onUserChanged = function() {
        var oldId = $scope.user.id;
        var promiseResp = $http.get(`http://${serverIp}/api/users/${$scope.user.id}`);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.user = data.data;
          $scope.user.id = oldId;
        });
    }
});

app.controller("editHackCtrl", function ($scope,$http) {
    $scope.hack = {
      name: "",
      description: "",
      beginDate: "",
      endDate: ""
    };
    $scope.msg = "Статус";
    $scope.onSend = function() {
        var promiseResp = $http.post(`http://${serverIp}/api/hackatons`,$scope.hack);
        promiseResp.then(function success(data,status,headers,config) {
            $scope.msg = "Успешно";
        });
    }
});

app.controller("editCaseCtrl", function (hackList,$scope,$http) {
    var vm = this;
    $scope.hackList = hackList;
    $scope.m_case = {hackatonId:-1, name:"", description:""};
    $scope.msg = "Статус";
    vm.reloadData = function () {  
        $route.reload();
    }
    $scope.onSend = function() {
        var promiseResp = $http.post(`http://${serverIp}/api/cases`,$scope.m_case);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.msg ="Успешно";
        });
    }
});

app.controller("editCheckCtrl", function (hackList,$scope,$http) {
    var vm = this;
    $scope.hackList = hackList;
    $scope.hackatonId = "";
    $scope.caseList = [];
    $scope.checkpoint = {caseId:-1, name:"", description:"", deadline:""};
    $scope.msg = "Статус";
    vm.reloadData = function () {  
        $route.reload();
    }
    $scope.onHackChange = function() {
        var promiseResp = $http.get(`http://${serverIp}/api/hackatons/${$scope.hackatonId}/cases`);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.caseList = data.data;
        });
    }
    $scope.onSend = function() {
        var promiseResp = $http.post(`http://${serverIp}/api/checkpoints`,$scope.checkpoint);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.msg ="Успешно";
        });
    }
});

app.controller("editCritCtrl", function (hackList,$scope,$http) {
    var vm = this;
    $scope.hackList = hackList;
    $scope.hackatonId = "";
    $scope.caseId = "";
    $scope.caseList = [];
    $scope.checkpointList = [];
    $scope.criteria = {scores:0, name:"", checkpointId:0};
    $scope.msg = "Статус";
    vm.reloadData = function () {  
        $route.reload();
    }
    $scope.onHackChange = function() {
        var promiseResp = $http.get(`http://${serverIp}/api/hackatons/${$scope.hackatonId}/cases`);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.caseList = data.data;
        });
    }
    $scope.onCaseChange = function() {
        var promiseResp = $http.get(`http://${serverIp}/api/cases/${$scope.caseId}/checkpoints`);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.checkpointList = data.data;
        });
    }
    $scope.onSend = function() {
        var promiseResp = $http.post(`http://${serverIp}/api/criterias`,$scope.criteria);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.msg ="Успешно";
        });
    }
});

app.controller("addScoreCtrl", function (hackList,$scope,$http) {
    var vm = this;
    $scope.hackList = hackList;

    $scope.teamList = [];
    $scope.hackatonId = "";
    $scope.checkpointId = "";
    $scope.criteriaList = [];
    $scope.checkpointList = [];

    $scope.criteria = {scores:0, teamId:0, criteriaId:""};
    $scope.msg = "Статус";
    vm.reloadData = function () {  
        $route.reload();
    }
    $scope.onHackChange = function() {
        $scope.criteria.scores = 0;
        $scope.checkpointList.length = 0;
        $scope.criteriaList.length = 0;
        $scope.teamList.length = 0;
        $scope.criteria.criteriaId = "";
        var promiseResp = $http.get(`http://${serverIp}/api/hackatons/${$scope.hackatonId}/teams`);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.teamList = data.data;
        });
    }
    $scope.onTeamChange = function() {
        $scope.checkpointList.length = 0;
        $scope.criteriaList.length = 0;
        $scope.criteria.criteriaId = "";
        if ($scope.criteria.teamId == null) return;
        var tmpCaseId = $scope.teamList.find(function(element) {
          return element.id == $scope.criteria.teamId;
        }).caseId;
        if (tmpCaseId == null)
            return;
        var promiseResp = $http.get(`http://${serverIp}/api/cases/${tmpCaseId}/checkpoints`);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.checkpointList = data.data;
        });
    }
    $scope.onCheckpointChange = function() {
        $scope.criteria.criteriaId = "";
        if ($scope.checkpointId == null) return;
        var promiseResp = $http.get(`http://${serverIp}/api/checkpoints/${$scope.checkpointId}/criterias`);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.criteriaList = data.data;
        });
    }
    $scope.onSend = function() {
        var promiseResp = $http.post(`http://${serverIp}/api/cases/${$scope.criteria.teamId}/addScores/${$scope.criteria.criteriaId}`,$scope.criteria);
        promiseResp.then(function success(data,status,headers,config) {
          $scope.msg ="Успешно";
        });
    }
});
