var CyberShop = angular.module('CyberShop', []);
CyberShop.controller('cyberController', function ($scope, myService) {
    $scope.errorwillcomehere = "";
    $scope.credentialserror = "";
    $scope.regexFirstName = "^[a-zA-Z]+$";
    $scope.loginButton = false;
    
    $scope.PostRegistrationDetail = function () {
        // here comes the power ;-)
            var a = $scope.a;
            var promisePost = myService.post(a);
        
        
        alert("successfully registered");

        //$scope.ng-disabled=true;

    }


        $scope.GetRegistrationDetail = function (foruser) {
            console.log("GetRegistrationDetail called");
            var alldetails = $scope.a;
            var promiseGetSingle = myService.checkUserExists(alldetails).then(function (data) {
                console.log("available");
                $scope.errorwillcomehere = "Username already exists!";
                $scope.loginButton = true;
                
                //alert("Username already exists!");
            }
                ,
                function (err) {
                    console.log("ERROR" + err);
                    $scope.errorwillcomehere = "Unique UserName!";
                    $scope.loginButton = false;
                    
                    
                }
            );  
            
        }


        $scope.GetLoginDetail = function (foruserlogin) {
 
            var alldetailsforlogin = $scope.c;
            // we're passing alldetailsforlogin to checkLogin function, it has to be present before
            var promiseGetSingle = myService.checkLogin(alldetailsforlogin).then(function () {

                $scope.credentialserror = "Credentials match!";
                
                alert("Credentials match!");
            }
                ,
                function (err) {
                    console.log("ERROR" + err);
                    $scope.credentialserror = "Incorrect Username/Password";
                    alert("Incorrect Username/Password");

                }
            );  
       }


        $scope.Justforcheck = function () {
            alert("check check");
        }
       
});
   
      