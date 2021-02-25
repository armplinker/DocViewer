// File Uploader support (for RadAsyncUpload)
// JavaScript source code
// https://demos.telerik.com/aspnet-ajax/asyncupload/examples/draganddrop/defaultcs.aspx?_ga=2.54565484.673458555.1593079189-965574850.1593079189

(function () {
    var $;
    var demo = window.demo = window.demo || {};

    var drop_area_bg_color = "#357A2B";
    var drop_area_normal_color = '';
    var drop_area_invisible_color = "#000000";


    demo.initialize = function () {
        $ = $telerik.$;

       // if (!Telerik.Web.UI.RadAsyncUpload.Modules.FileApi.isAvailable()) {
      //      $(".qsf-demo-canvas").html("<strong>Your browser does not support Drag and Drop. Please take a look at the info box for additional information.</strong>");
      //  }
    //    else {
            $(document).bind({ "drop":function (e) { e.preventDefault(); } });

            var dropZone1 = $(document).find(".DropZone1");
            dropZone1.bind({ "dragenter":function (e) { dragEnterHandler(e, dropZone1); } })
                .bind({ "dragleave":function (e) { dragLeaveHandler(e, dropZone1); } })
                .bind({ "drop":function (e) { dropHandler(e, dropZone1); } });

            //var dropZone2 = $(document).find("#DropZone2");
            //dropZone2.bind({ "dragenter":function (e) { dragEnterHandler(e, dropZone2); } })
            //    .bind({ "dragleave":function (e) { dragLeaveHandler(e, dropZone2); } })
            //    .bind({ "drop":function (e) { dropHandler(e, dropZone2); } });
       // }
    };

    function dropHandler(e, dropZone) {
        dropZone[0].style.backgroundColor = drop_area_bg_color;//"#357A2B";
    }

    function dragEnterHandler(e, dropZone) {
        var dt = e.originalEvent.dataTransfer;
        var isFile = (dt.types !== null && (dt.types.indexOf ? dt.types.indexOf('Files') !== -1 :dt.types.contains('application/x-moz-file')));
        if (isFile || $telerik.isSafari5 || $telerik.isIE10Mode || $telerik.isOpera)
            dropZone[0].style.backgroundColor = drop_area_invisible_color;// "#000000";
    }

    function dragLeaveHandler(e, dropZone) {
        if (!$telerik.isMouseOverElement(dropZone[0], e.originalEvent))
            dropZone[0].style.backgroundColor = drop_area_bg_color;//"#357A2B";
    }


})();
