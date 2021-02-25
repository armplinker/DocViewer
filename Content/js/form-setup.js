window.$ = $telerik.$;
var defaultFactor = 0.98;

function getViewPort(winArg) {

    const win = winArg,
        w = winArg.innerWidth,
        h = winArg.innerHeight;


    //// jQuery
    //var e = winArg,
    //    h = $(winArg).height(),
    //    w = $(winArg).width();
    //window.log_debug(`ViewPort:window:${winArg.name} w:${w} x h:${h}`);
    return { width:w, height:h };

}

function getDocumentSize(documentArg) {

    const docElem = documentArg.documentElement,
        w = docElem.clientWidth,
        h = docElem.clientHeight;


    // jQuery
    //var e = documentArg,
    //    h = $(documentArg).height(),
    //    w = $(documentArg).width();

    //window.log_debug(`Document Size:document:${documentArg.name} w:${w} x h:${h}`);
    return { width:w, height:h };

}

function getScreenSize(winArg) {

    const e = winArg,
        hMax = winArg.screen.height,
        wMax = winArg.screen.width,
        hAvail = winArg.screen.availHeight,
        wAvail = winArg.screen.availWidth;

    return { wMax:wMax, hMax:hMax, wAvail:wAvail, hAvail:hAvail };
}

function setScreenDimensions(winArg, factor) {

    if (winArg) {
        var $ = $telerik.$;

        var vp = getViewPort(winArg);
        log_debug(`In form-setup - ViewPort:w:${vp.width} x h:${vp.height}`);

        var docDims = winArg.getDocumentSize(winArg.document);
        log_debug(`In form-setup - Document:w:${docDims.width} x h:${docDims.height}`);

        var screenDims = getScreenSize(winArg);
        log_debug(
            `In form-setup - Screen size:Max:w:${screenDims.wMax} x h:${screenDims.hMax} - available:${
            screenDims.wAvail} x h:${screenDims.hAvail}`);


        var currentDimensions = null;

        if (IsRadWindow(winArg)) {
            var radWin = winArg.radWindow; // GetRadWindow(winArg);

            // must be a radwindow to use get_width and get_height

            if (radWin) //!== null && radWin !== 'undefined')
                currentDimensions =
                    `RADWINDOW:before setScreenDimensions (radWindow):w${radWin.get_width()} x h:${
                    radWin.get_height()}`;
        } else
            currentDimensions =
                `WINDOW:before setScreenDimensions (browserWindow):w${winArg.width} x h:${winArg.height}`;


        log_debug(currentDimensions);

        var callString =
            `SCREEN_DIMENSIONS;${screen.width};${screen.height};${screen.availWidth};${screen.availHeight}`;

        window.log_debug(callString);

        winArg.moveTo(0, 0);
        winArg.resizeTo(screen.availWidth, screen.availHeight);

        var
            doc = document,
            docElem = doc.documentElement,
            body = doc.getElementsByTagName('body')[0],
            w = winArg.innerWidth || docElem.clientWidth || body.clientWidth,
            h = winArg.innerHeight || docElem.clientHeight || body.clientHeight;
        var calculatedDimensions =
            w * (factor !== null && factor !== 'undefined' && factor > 0 ? factor :defaultFactor);

        log_debug(`Calculated:${calculatedDimensions}`);
        var ajaxRequest = `CLIENT_WIDTH:${calculatedDimensions}`;

        log_debug(ajaxRequest);
    }

    return (ajaxRequest !== null && ajaxRequest !== "undefined") ? ajaxRequest :"";
}

function GetRadWindow(winArg, winOpenArg) {
    log_debug(`In form-setup GetRadWindow(${winArg}, ${winOpenArg})`);
    var $ = $telerik.$;
    if (!winArg) return null;
    var oWnd = null;

    if (winArg.radWindow)
        oWnd = winArg.radWindow;
    else if (winArg.frameElement && winArg.frameElement.radWindow)
        oWnd = winArg.frameElement.radWindow;

    // oWindow must exist
    if (oWnd !== null && oWnd !== "undefined") {
        if (winOpenArg !== null && winOpenArg !== "undefined") {
            if (oWnd) {
                oWnd.argument = winOpenArg; //'EXPIRATION';
            }
        }
    }
    return oWnd;
}

function IsRadWindow(winArg) {
    log_debug("In IsRadWindow");
    log_debug(`In IsRadWindow - winarg = ${winArg}`);
    if (winArg !== null && winArg !== "undefined") {
        var isIt = false;
        if (winArg.radWindow) {
            isIt = true;
        }
        log_debug(`In IsRadWindow - isit - ${isIt}`);
        return isIt;
    }
    return false;
}

function CheckWnd(winArg) {
    log_debug('In CheckWnd');
    var wnd = GetRadWindow(winArg, "EXPIRATION");
    if (wnd) {
        if (IsRadWindow(wnd)) {
            window.log_debug(`This page ${wnd.name} is loaded in a RadWindow (in form-setup.js)`);
        }
        else {
            window.log_debug(`This page ${wnd.name} is loaded in the browser (in form-setup.js)`);
        }
    }
    return false;
}


