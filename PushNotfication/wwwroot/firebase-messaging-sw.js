importScripts('https://www.gstatic.com/firebasejs/10.5.0/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/10.5.0/firebase-messaging-compat.js');

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
messaging.onBackgroundMessage(function (payload) {
    console.log("im here " + payload)
    var title = payload.data.title;

    var options = {
        body: payload.data.body,
        icon: payload.data.icon,
        image: payload.data.image,
        data: {
            time: new Date(Date.now()).toString(),
            click_action: payload.data.click_action
        }

    };
    return self.registration.showNotification(title, options);


});


self.addEventListener('notificationclick', function (event) {

    var action_click = event.notification.data.click_action;
    event.notification.close();

    event.waitUntil(
        clients.openWindow(action_click)
    );
});