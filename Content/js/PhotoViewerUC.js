// JavaScript source code
//window.$ = $telerik.$;

//var oWindow = GetRadWindow();

function ResolveUrl(url) { return url.replace("~/", window.baseUrl); }

//This code is used to provide a reference to the radwindow "wrapper"
function GetRadWindow() {

    // ARMarshall, ARM LLC 20160504 - added null check
    if (!window) return null;
    var oWnd = null;
    if (window.radWindow) oWnd = window.radWindow;
    else if (window.frameElement && window.frameElement.radWindow) oWnd = window.frameElement.radWindow;
    return oWnd;
}

function callBackFnPromptReloadDocuments(changes) {
    //https://stackoverflow.com/questions/2797560/set-a-callback-function-to-a-new-window-in-javascript
    var hFld = window.$get('HiddenFieldDocListRefreshNeeded');
    if (changes) {
        hFld.value = changes.toLowerCase() === "true"; // "True"

        //var btn = $find('<%=RadButtonRefresh.ClientID %>');
        //$(btn).addClass('enabled');
        //$(btn).addClass('visible');
        //$(btn).attr.remove('disabled');

        const winMgr = window.GetRadWindowManager();
        var ok = winMgr.radalert(
            `<div class='flex flex-row items-center h-auto GoodNews GoodNews-sm space-x-2 items-center w-full'>
                <i class='fas fa-check button-icon button-icon-green fa-2x object-contain' aria-hidden="true">&nbsp;</i>
                <span>New documents added. Please click <strong>Refresh Documents</strong> to reload the document list.</span>
            </div>`, 450, 150, 'New Documents Added');
        // var oAlert = window.radalert("Please click refresh to reload the documents");
    }
    else {
        hFld = false;
    }
    return;
}

function OnUploadPageClientCloseHandler(sender, args) {
    var data = args.get_argument();
    if (data) {
        callBackFnPromptReloadDocuments(data);
    }
}

function PVUC_onAddPhotosClientClicking(sender, args) {
    var $ = $telerik.$;

    if (!window) return null;
    if (!document) return null;
    var url = document.getElementById("HiddenFieldPhotoUploaderUrl").value;


    //var btn = $find("<%=RadButtonRefresh.ClientID %>");

    //$(btn).addClass('disabled');
    //$(btn).addClass('invisible');
    //$(btn).attr.remove('enabled');
    //$(btn).attr.add('disabled');

    if (!(url === "" || url.length === 0)) {
        const winMgr = window.GetRadWindowManager();
        url = ResolveUrl(url);

        const oWnd = winMgr.open(url, "UploadDialogWindow");
        ////https://stackoverflow.com/questions/2797560/set-a-callback-function-to-a-new-window-in-javascript
        //oWnd.onclose = function () {
        //    oWnd.RunCallbackFunction = function () {
        //        callBackFnPromptReloadDocuments();
        //        oWnd.CloseWindow();
        //    };
        //};

        oWnd.set_modal(true);
        oWnd.set_centerIfModal(true);
        oWnd.set_keepInScreenBounds(true);
        oWnd.set_destroyOnClose(false);
        const winHeight = oWnd.get_height();
        if (winHeight && winHeight > 0) {
            oWnd.set_height(winHeight);
        }
        else {
            oWnd.set_height(1100);// for pleasant proportions
        }
        const winWidth = oWnd.get_width();

        if (winWidth && winWidth > 0) {
            oWnd.set_width(winWidth);
        }
        else {
            oWnd.set_width(800); // for pleasant proportions
        }

        oWnd.setSize(oWnd.get_width(), oWnd.get_height());
        oWnd.argument = sender.ID;

    }
    return true;
}

function PVUC_confirmPhotoDeleteCallbackFn(arg) {
    if (arg) {

        return true;
    }
    return false;
}

function PVUC_onDeletePhotoClientClicking(sender, args) {
    //debugger
    if (!window) return null;
    const winMgr = window.GetRadWindowManager();
    const ok = winMgr.radconfirm("Are you sure that you want to delete the selected photos or documents?", "PVUC_confirmPhotoDeleteCallbackFn", 300, 200, null, "Confirm");
    args.set_cancel(!ok);
} 
