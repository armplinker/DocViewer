//function NotesEditingPanel(client_id_in) {
//    var oWin = window;
//    var client_id = client_id_in;

//    this.init = function () {

//      return true;
//    };

//subscribe to window resize event so everything resizes properly
window.onresize = function () {
  var $ = $telerik.$;
    var oTxtBox = $find('<%= TbxNotes.ClientID %>');
    if (oTxtBox)
        oTxtBox.repaint();
};


//This code is used to provide a reference to the radwindow "wrapper"
function GetRadWindow() {
    if (!window) return null;
    var oWindow = null;
    if (window.radWindow) oWindow = window.radWindow;
    else if (window.frameElement && window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
    return oWindow;
}

// show the popup editor on click of pencil icon
//ShowPopupModalEditor_<%= ClientID %>
function ShowPopupModalEditor() {
  var $ = $telerik.$;
    var oInput = document.getElementById('<%= TbxNotes.ClientID %>');
    var oText = '';
    var emptyMessage = $find("<%= TbxNotes.ClientID %>").get_emptyMessage();// every time we invoke this
    //'value' will send the EmptyMessage if nothing has been typed, which we do not want to see in the popup, so we null it out.
    if (oInput && (oInput.value !== emptyMessage)) {
        oText = oInput.value;
    }
    var arg = new Object();
    arg.TextValue = oText;
    arg.SourceId = '<%= TbxNotes.ClientID %>';
    arg.MaxChars = oInput.maxLength; // chars


    //var oWnd = oManager.open("Administration/ChangePassword.aspx?status=0", null);
    //if (oWnd != null) {
    //    oWnd.argument = null;
    //    oWnd.autoSize(true);
    //    oWnd.center();
    //}
    var oManager = window.GetRadWindowManager();
    var oWnd = oManager.open(null, 'NotesPanelPopupEditorWindow_' + client_id, null, 770, 450, null, null);


    if (oWnd) {
        oWnd.setUrl('NotesPanelPopupEditor.aspx?shrink=no');
        oWnd.argument = arg;

        //oWnd.autoSize(false);

        //oWnd.set_autoSizeBehaviors(Telerik.Web.UI.WindowAutoSizeBehaviors.Width + Telerik.Web.UI.WindowAutoSizeBehaviors.Height);

        //oWnd.center();
        oWnd.set_modal(true);
        oWnd.set_centerIfModal(true);
        oWnd.set_keepInScreenBounds(true);
        oWnd.set_destroyOnClose(false);

        //window.testAutoSize = true;


        const winHgt = oWnd.get_height();
        if (winHgt && winHgt > 0) {
            oWnd.set_height(winHgt);
        } else {
            oWnd.set_height(390);
        }
        const winWdth = oWnd.get_width();

        if (winWdth && winWdth > 0) {
            oWnd.set_width(winWdth);
        } else {
            oWnd.set_width(860);
        }

        oWnd.setSize(oWnd.get_width(), oWnd.get_height());



        if (oWnd.get_title() === "")
            oWnd.set_title('Notes');// default title
        oWnd.set_visibleStatusbar(false);
        oWnd.set_showContentDuringLoad(false);
        //if (testAutoSize) {
        //    oWnd.autoSize(true);
        //}

        oWnd.setActive(true);
    }
    return false;
}

// copy the changed text in the buffer to the target textbox
// function ClosePopupModalEditor_<%= ClientID %>
function ClosePopupModalEditor(sender, eventArgs) {
    var $ = $telerik.$;

    if (!sender) //== null)
        return false;
    var arg = sender.argument;
    if (arg && arg.Updated) {
        //var oInput = document.getElementById(arg.SourceId);
        log_debug(`arg.SourceID = ${arg.SourceId}`);
        var oInput = document.getElementById('<%= TbxNotes.ClientID %>');
        log_debug(`TbxNotes.ClientID = ${'<%= TbxNotes.ClientID %>'}`);
        log_object(oInput);
        // avoid returning overly long text from editor
        var txt = arg.TextValue;
        var maxLen = oInput.maxLength;
        if (maxLen && maxLen > 0) {
            if (txt && txt.length > maxLen)
                txt = txt.substring(0, maxLen);
        };
        oInput.value = txt; //ARMarshall, ARM LLC made this change from innerText to textContent
        oInput.innerText = txt;
        oInput.textContent = txt;

        oInput.focus();
    }
    return false;
}

// function BeforeClosePopupModalEditor_<%= ClientID %>
function BeforeClosePopupModalEditor(sender, args) {
    var $ = $telerik.$;

    var resetWinArgument = true;

    function confirmCallback(arg) {
        if (arg) {
            sender.remove_beforeClose(BeforeClosePopupModalEditor);
            if (resetWinArgument === true)
                sender.argument = null;
            sender.close();
            sender.add_beforeClose(BeforeClosePopupModalEditor);
        }
    }

    const oWnd = sender;
    // get a reference to the popup editor RadWindow
    const winMgr = oWnd.get_windowManager();
    const editorDialog = winMgr.getWindowByName('NotesPanelPopupEditorWindow_' + client_id);
    if (editorDialog) {
        // by using get_contentFrame, call the predefined function
        const changes = (editorDialog.get_contentFrame().contentWindow.get_isDirty() === true); // look at the javascript variable....
        const saved = (editorDialog.get_contentFrame().contentWindow.get_changesSaved() === true);
        if (changes && !saved) {
            resetWinArgument = true; // presumably, but the callback actually nulls it out
            window.radconfirm('Are you sure you want to close the notes editing window without saving your changes?', confirmCallback, 450, 200, null, "Confirm Cancel Changes");
        }
        else {
            resetWinArgument = false; // keep the new data
            confirmCallback(true);
        }
    }

    args.set_cancel(true);
    return false;
}

//}