// JavaScript source code
//$(document).ready(function () {
//debugger;
var imCtrl = $(".load-rating-methods-picklist");
if (!(typeof imCtrl === undefined)) {
    imCtrl.each(
        function () {
            console.log($(this).prop("id"));
            if (!($(this).prop("id").match(/ddl/) === null)) { // found
                for (let [key, value] of Object.entries(this)) {
                    console.log(key + ' : ' + value);

                    //if (key === 'control') {
                    //    console.log('FOUND: ' + $(this).prop('id'));
                    //}
                    if (key === "value") {
                        console.log(value);
                        var cbxItem = new Telerik.Web.UI.RadComboBoxItem();
                        cbxItem.set_value(value);
                        // ARMarshall, ARM LLC - 20190809 - added explicit window. reference
                        window.setup_dependent_lr_controls(cbxItem);
                    }
                }
            }
        }
    );
}
//}
//);
