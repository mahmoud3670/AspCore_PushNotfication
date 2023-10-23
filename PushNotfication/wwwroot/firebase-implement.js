 let notificationCount=0
        // Initialize Firebase
        var config = {
            apiKey: "AIzaSyBZMLMAwYOeQ8KMDQBK2HkoUnM548CKCfk",
            authDomain: "testpush-327ec.firebaseapp.com",
            projectId: "testpush-327ec",
            storageBucket: "testpush-327ec.appspot.com",
            messagingSenderId: "816364838266",
            appId: "1:816364838266:web:02161e965bb610e2a34ac3"
        };
        firebase.initializeApp(config);
     
        // Retrieve Firebase Messaging object.
        const messaging = firebase.messaging();
       
        Notification.requestPermission().then(function () {
            //console.log('Notification permission granted.');

            if (isTokenSentToServer()) {

                 console.log("Token Already sent");
            } else {
                getRegisterToken();
            }

            // TODO(developer): Retrieve an Instance ID token for use with FCM.
            // ...
        }).catch(function (err) {
            console.log('Unable to get permission to notify.', err);
        });


        function getRegisterToken() {
            // Get Instance ID token. Initially this makes a network call, once retrieved
            // subsequent calls to getToken will return from cache.
            messaging.getToken().then(function (currentToken) {
                if (currentToken) {
                    saveToken(currentToken);
                    console.log(currentToken);
                    sendTokenToServer(currentToken);
                    // updateUIForPushEnabled(currentToken);
                } else {
                    // Show permission request.
                    console.log('No Instance ID token available. Request permission to generate one.');
                    // Show permission UI.
                    // updateUIForPushPermissionRequired();
                    setTokenSentToServer(false);
                }
            }).catch(function (err) {
                console.log('An error occurred while retrieving token. ', err);
                //showToken('Error retrieving Instance ID token. ', err);
                setTokenSentToServer(false);
            });
        }

        function setTokenSentToServer(sent) {
            window.localStorage.setItem('sentToServer', sent ? '1' : '0');
        }

        function sendTokenToServer(currentToken) {
            if (!isTokenSentToServer()) {
                console.log('Sending token to server...');
                // TODO(developer): Send the current token to your server.
                setTokenSentToServer(true);
            } else {
                console.log('Token already sent to server so won\'t send it again ' +
                    'unless it changes');
            }
        }
        function isTokenSentToServer() {
            return window.localStorage.getItem('sentToServer') === '1';
        }

        function saveToken(currentToken) {
            let dataObject = {
                id:0,
                FmcToken:currentToken
            }
            jQuery.ajax({
                data: JSON.stringify(dataObject),
                type: "Post",
                url: `api/Notification/setToken`,
                contentType: "application/json charset=utf-8",
                dataType: "json",
                success: function (result) {
                    console.log(result);
                }

            });
        }

        messaging.onMessage(function (payload) {
            console.log('Message received. ', payload);
            //alert(payload.notification.title)
            var title = payload.notification.title;

            var options = {
                body: payload.notification.body,
                icon: payload.notification.icon,
                //image: payload.data.image,
                // data: {
                //     time: new Date(Date.now()).toString(),
                //     click_action: payload.data.click_action
                // }
            };
            var myNotification = new Notification(title, options);
           
            notificationCount++
            $("#not-count").empty()
            $("#not-count").removeClass("d-none")
            $("#not-count").append(notificationCount)
            //return self.registration.showNotification(myNotification);

           
        });
