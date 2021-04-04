(function ($, kendo) {
  $.extend(true, kendo.ui.validator, {
    rules: {
      mvcremotevalidation: function (input) {
        if (input.is("[data-val-remote]") && input.val() != "") {
          var remoteURL = input.attr("data-val-remote-url");
          var valid;
          $.ajax({
            async: false,
            url: remoteURL,
            type: "GET",
            dataType: "json",
            data: validationData(input, this.element),
            success: function (result) {
              if (result == true || result == false) {
                valid = result;
              }
              else {
                input[0].dataset.valRemote = result;
                valid = false;
              }
            },
            error: function () {
              valid = false;
            }
          });
          return valid;
        }

        return true;
      }
    },
    messages: {
      mvcremotevalidation: function (input) {
        return input.attr("data-val-remote");
      }
    }
  });
  function validationData(input, context) {
    var fields = input.attr("data-val-remote-additionalFields").split(",");
    var name = input.prop("name");
    var prefix = name.substr(0, name.lastIndexOf(".") + 1);
    var fieldName;
    var data = {};
    for (var i = 0; i < fields.length; i++) {
      fieldName = fields[i].replace("*.", prefix);
      data['field' + i] = $("[name='" + fieldName + "']", context).val();
    }
    return data;
  }
})(jQuery, kendo);


function base64ToArrayBuffer(base64) {
  var binaryString = window.atob(base64);
  var binaryLen = binaryString.length;
  var bytes = new Uint8Array(binaryLen);
  for (var i = 0; i < binaryLen; i++) {
    var ascii = binaryString.charCodeAt(i);
    bytes[i] = ascii;
  }
  return bytes;
}

function onGridRead(e) {

}
function onGridRequestEnd(e) {
  if (e.type == "destroy" || e.type == "create") {

  }
}
function sync_handler(e) {
  this.read();
}
function error_handler(e) {
  if (e.errors) {
    var message = "Errors:\n";
    $.each(e.errors, function (key, value) {
      if ('errors' in value) {
        $.each(value.errors, function () {
          message += this + "\n";
        });
      }
    });
    alert(message);
  }
}

var pageX, pageY;

$(document).mousemove(
  function (e) {
    pageX = e.pageX;
    pageY = e.pageY;
  });

$('#tooltipped li[title]').hover(
  function () {
    var tip = $('<div />')
      .addClass('tooltip')
      .text($(this).attr('title'))
      .css({
        'position': 'absolute',
        'top': pageY,
        'left': pageX
      });
    $(tip).appendTo($(this));
    $(this).mousemove(
      function () {
        $('.tooltip').css(
          {
            'top': pageY,
            'left': pageX
          });
      });
  },
  function () {
    $('.tooltip').remove();
  });
function windowClose(e) {
  $("#popupWindowDiv").html("");
  $("#windowPopUp").data("kendoWindow").setOptions({
    title: ""
  });
}
function gridTexter(e) {
  $("a.k-grid-update")[0].innerHTML = "Kaydet";
}
