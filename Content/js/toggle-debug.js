//toggle-debug.js - enable and disable DEBUG for related scripts - add this as reference....
var $ = $telerik.$;

var DEBUGLEVEL = false;
var INFOLEVEL = true;
var DIRLOG = true;
var WARNLEVEL = true;
var ERRORLEVEL = true;


if (!window.console) window.console = {};

function log_debug(message) {
    if (DEBUGLEVEL === true)
        console.debug(message);
    return false;
}

function log_info(message) {
    if (INFOLEVEL === true)
        console.info(message);
    return false;
}

function log_object(object) {
    if (object !== null && object !== "undefined") {
        if (DIRLOG === true)
            console.dir(object);
    }
    else
        console.warn("Call to log_object-  object is null or undefined");
    return false;
}

function log_warn(message) {
    if (WARNLEVEL === true)
        console.warn(message);
    return false;
}

function log_error(message) {
    if (ERRORLEVEL === true)
        console.error(message);
    return false;
}

function toggle_logging(onOff) {
    DEBUGLEVEL = onOff;
    INFOLEVEL = onOff;
    DIRLOG = onOff;
    WARNLEVEL = onOff;
    ERRORLEVEL = onOff;
    console.log(`Setting logging enabled to: ${onOff}`);
    show_logging_levels();
    return false;
}

function show_logging_levels() {
    console.info(`Debug: ${DEBUGLEVEL}`);
    console.info(`Info: ${INFOLEVEL}`);
    console.info(`Warn: ${WARNLEVEL}`);
    console.info(`Object (dir()): ${DIRLOG}`);
    console.info(`Error: ${ERRORLEVEL}`);
    return false;
}

window.setTimeout(function () {
    "use strict";
    $telerik.$(function () {
        toggle_logging(true);
        show_logging_levels();
        log_debug("Setting up logging functions (in toggle-debug.js)");
    }
    );
}, 1000);
