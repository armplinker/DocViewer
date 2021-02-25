// beat the heart
// 'times' (int):nr of times to beat

var $ = $telerik.$;

function beatHeart(times) {


    var hb = $(".heartbeat");
    var interval = 100;
    if (hb) {
        interval = setInterval(function () {
            hb.fadeIn(100,
                function () {
                    hb.fadeOut(300);
                });
        },
            1000);
    } // beat every second

    window.log_debug(`Keep-alive heartbeat interval is ${interval} msecs`);

    // after n times, let's clear the interval (adding 100ms of safe gap)
    window.setTimeout(function () { clearInterval(interval); }, 1000 * times + 100);

    return false;
}

(function () {
    "use strict";
    $(document).ready(function () {
       // window.log_debug("Setting keep-alive interval (in heart-beat.js)");
        // just keeping beating 2 times, each 15 seconds
        window.setInterval(function () { beatHeart(1); }, 30000);
    });
})($telerik.$);