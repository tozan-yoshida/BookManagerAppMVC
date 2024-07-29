﻿////------------------- DataTables　------------------//
//$(document).ready(function () {
//    $('#myTable').DataTable({
//        "language": {           // 日本語表示
//            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/ja.json"
//        },
//        lengthChange: true,     // 表示件数
//        info: false,            // 総件数
//        scrollX: true
//    });

//    // 横スクロール禁止
//    $('#myTableNoScrollX').DataTable({
//        "language": {           // 日本語表示
//            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/ja.json"
//        },
//        lengthChange: true,     // 表示件数
//        info: false,            // 総件数
//        order: [[1, "desc"]],   // 作成日降順
//    });

//    // 出荷指示取込エラー履歴(作成日降順)
//    $('#myTableShippingImportError').DataTable({
//        "language": {           // 日本語表示
//            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/ja.json"
//        },
//        lengthChange: true,     // 表示件数
//        info: false,            // 総件数
//        scrollX: true,
//        order: [[0, "desc"]],   // 作成日降順
//    });

//    // 出荷計画照会(取込日時降順)
//    $('#myTableShippingPlanInquiry').DataTable({
//        "language": {           // 日本語表示
//            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/ja.json"
//        },
//        lengthChange: true,     // 表示件数
//        info: false,            // 総件数
//        scrollX: true,
//        order: [[1, "desc"]],   // 取込日時降順
//    });

//    // ページ上のすべてのファイル入力にfileselectイベント付与
//    $(document).on("change", ":file", function () {
//        var input = $(this),
//            numFiles = input.get(0).files ? input.get(0).files.length : 1,
//            label = input
//                .val()
//                .replace(/\\/g, "/")
//                .replace(/.*\//, "");
//        input.trigger("fileselect", [numFiles, label]);
//    });

//    // fileselectイベント監視
//    $(":file").on("fileselect", function (event, numFiles, label) {
//        var input = $(this)
//            .parents(".input-group")
//            .find(":text"),
//            log = numFiles > 1 ? numFiles + " files selected" : label;

//        if (input.length) {
//            input.val(log);
//        } else {
//            if (log) alert(log);
//        }
//    });

//});
////--------------------------------------------------------//


////------------------- パスワード変更　------------------//
//$("#changePasswordForm").submit(function () {
//    $(".changePassword .alert-success").css("display", "none");
//});

//// パスワード表示アイコン
//$('#eye-change-pass').click(function () {
//    if ($(this).hasClass('fa-eye')) {
//        $(this).removeClass('fa-eye');
//        $(this).addClass('fa-eye-slash');
//        $('#password-field').attr('type', 'text');
//    } else {
//        $(this).removeClass('fa-eye-slash');
//        $(this).addClass('fa-eye');
//        $('#password-field').attr('type', 'password');
//    }
//});

//$('#eye-login').click(function () {
//    if ($(this).hasClass('fa-eye')) {
//        $(this).removeClass('fa-eye');
//        $(this).addClass('fa-eye-slash');
//        $('.login-text').attr('type', 'text');
//    } else {
//        $(this).removeClass('fa-eye-slash');
//        $(this).addClass('fa-eye');
//        $('.input-password .login-text').attr('type', 'password');
//    }
//});

////------------------- Excel取込　------------------//
//function onUploadFile(page) {
//    $("#FailMsg").text("");
//    $('#import-res').empty;
//    $('#import-res').removeClass('text-danger');

//    // FormDataオブジェクト利用
//    var formData = new FormData(document.querySelector('#' + page + ''));
//    var IsFirst = true;
//    for (var file of formData) {
//        if (IsFirst) {
//            if (file[1]["size"] <= 0) {
//                $('#import-res').text('ファイルが選択されていません。');
//                $("#import-res").show()
//                $("#import-res").addClass('text-danger');
//                $("#ErrorBlock").hide()
//                return false;
//            }
//            else {
//                $("#import-res").hide()
//                $("#ErrorBlock").hide()
//                IsFirst = false;
//            }
//        }

//    }

//    const dialog = document.getElementById("ImportModel");

//    if (dialog) {
//        dialog.parentNode.removeChild(dialog);
//    }

//    var modelTitle = "";
//    if (page == 'shipping-plan-import-upload-form')
//        modelTitle = "出荷計画取込";

//    if (page == 'user-master-upload-form')
//        modelTitle = "ユーザーマスター";

//    if (page == 'shipping-master-upload-form')
//        modelTitle = "出荷レーンマスター";

//    if (page == 'm-routes-master-upload-form')
//        modelTitle = "運行便マスター";

//    $('body').append(
//        '<div class="modal fade" id="ImportModel" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">' +
//        '    <div class="modal-dialog modal-dialog-centered" role="document">' +
//        '        <div class="modal-content">' +
//        '            <div class="modal-header">' +
//        '                <h5 class="modal-title" id="exampleModalCenterTitle">' + modelTitle + '</h5>' +
//        '                <button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
//        '                    <span aria-hidden="true">&times;</span>' +
//        '                </button>' +
//        '            </div>' +
//        '            <div class="modal-body">' +
//        '                <p>ファイル取込を行います。よろしいですか？</p > ' +
//        '            </div>' +
//        '            <div class="modal-footer">' +
//        '                <button type="button" class="btn btn-secondary" data-dismiss="modal">キャンセル</button>' +
//        '                <button type="button" class="btn btn-primary">OK</button>' +
//        '            </div>' +
//        '        </div>' +
//        '    </div>' +
//        '</div>'
//    );

//    $('#ImportModel').modal('show');

//    $('#ImportModel').on('hidden.bs.modal', function (e) {
//        //なし
//    });

//    $('#ImportModel .btn-secondary').click(function () {
//        $('#ImportModel').modal('hide');
//        return false;
//    });

//    $('#ImportModel .btn-primary').click(function () {
//        $('#ImportModel').modal('hide');

//        var importUrl = document.getElementById('import_action_url').value;
//        formData.append("userName", "@User.Identity.Name");
//        showLoading() 
//        $.ajax({
//            url: importUrl,
//            method: 'post',
//            data: formData,
//            processData: false,
//            contentType: false
//        }).done(function (response) {
//            if (response.res == "OK") {
//                if (response.data != "OK") {
//                    let data = JSON.parse(response.data);
//                    $("#import-res").addClass('text-danger');
//                    $("#ErrorBlock").show()
//                    RenderErrorBlock(data);
//                    $('#' + page + '')[0].reset();
//                    hideLoading()
//                }
//                else {
//                    hideLoading()
//                    AlertMessage('', '' + modelTitle + '', '登録が完了しました。', null, null);
//                }
//            }
//            else {
//                hideLoading()
//                $("#ErrorBlock").hide()
//                $("#import-res").show()
//                $("#import-res").addClass('text-danger');
//                $("#import-res").text(response.error);
//                $('#' + page + '')[0].reset();
//            }
//        }).fail(function (jqXHR, textStatus, errorThrown) {
//            hideLoading()
//            console.log("jqXHR", jqXHR.status);
//            console.log("textStatus", textStatus);
//            console.log("errorThrown", errorThrown.message);
//            $("#import-res").addClass('text-danger');
//            $("#import-res").show()
//            AlertMessage('bg-danger', 'エラー', 'E3003 サーバーに接続できませんでした。<br> ' + 'HttpRequest : ' + jqXHR.status + '<br> ' + 'textStatus : ' + textStatus, null, null);
//            $('#' + page + '')[0].reset();
//        });
//    });
//}

//function showLoading() {
//    $("#file-upload").addClass('btn-disable')
//    $("#file-upload .text").text("取込中")
//    $("#file-upload i").removeClass('fa-solid fa-file-export')
//    $("#file-upload i").addClass('fas fa-spinner fa-pulse')
//}

//function hideLoading() {
//    $("#file-upload").removeClass('btn-disable')
//    $("#file-upload .text").text("取込")
//    $("#file-upload i").addClass('fa-solid fa-file-export')
//    $("#file-upload i").removeClass('fas fa-spinner fa-pulse')
//}
//function showSearching() {
//    $("#searchButton").addClass('btn-disable')
//    $("#searchButton .text").text("検索中")
//}

//function hideSearching() {
//    $("#searchButton").removeClass('btn-disable')
//    $("#searchButton .text").text("検索")
//}
//function showExporting() {
//    $(".btn-export-file").addClass('btn-disable')
//    $(".btn-export-file .text").text("出力中")
//}

//function hideExporting() {
//    $(".btn-export-file").removeClass('btn-disable')
//    $(".btn-export-file .text").text("出力")
//}

//// データエラー表示
//function RenderErrorBlock(data) {
//    let html = "";
//    for (let i = 0; i < data.length; i++) {
//        html += '<span class="text-danger">' + data[i] + '</span>';
//    }
//    document.getElementById('ErrorBlock').innerHTML = html;
//}
////--------------------------------------------------------//

////------------------- Excel出力　------------------//
//async function onExportExcel(page) {
//    event.preventDefault();
//    $("#ErrorBlock").text("");
//    $('#import-res').text("");
//    showExporting()
//    const response = await fetch('' + page + '/ExportExcel', {
//        method: 'GET', // *GET, POST, PUT, DELETE, etc.
//        headers: {
//            'Content-Type': 'application/json'
//        },
//    });
//    var { data, error } = await response.json();
//    if (error) {
//        //エラー
//        $("#ErrorBlock").show()
//        $("#ErrorBlock").text(error);
//    } else {
//        var { contentType, fileContents, fileDownloadName } = data;
//        {
//            const link = document.createElement("a");
//            link.href = `data:${contentType};base64,${fileContents}`;
//            link.download = fileDownloadName;
//            link.click();
//        }
//    }
//    hideExporting()
//}

//// 条件あり
//function onExportExcelByCondition(page) {
//    $("#ErrorBlock span").remove()
//    var startDate = $("#startDate").val();
//    var endDate = $("#endDate").val();
//    showExporting()
//    formData = { startDate: startDate, endDate: endDate };
//    $.ajax({
//        type: 'POST',
//        url: '' + page + '/ExportExcel',
//        data: formData,
//        success: function (response) {
//            if (response.res == "OK") {
//                var { contentType, fileContents, fileDownloadName } = response.data;
//                {
//                    const link = document.createElement("a");
//                    link.href = `data:${contentType};base64,${fileContents}`;
//                    link.download = fileDownloadName;
//                    link.click();
//                }
//            } else {
//                let html = '<span class="text-danger">' + response.error + '</span>';
//                document.getElementById('ErrorBlock').innerHTML = html;
//            }
//            hideExporting()
//        },
//        error: function (request, status, error) {
//            hideExporting()
//            alert(request.responseText);
//        }
//    });
//}
////--------------------------------------------------------//


//// モーダル表示
//function AlertMessage(type, title, message, isRedirect, urlRedirect) {
//    const dialog = document.getElementById("AlertDialogId");
//    if (dialog) {
//        dialog.parentNode.removeChild(dialog);
//    }
//    var actionAfter = "OK";
//    if (isRedirect) {
//        message = "登録が完了しました。ユーザーマスター画面へ戻ります。";
//        actionAfter = "OK";
//    }
//    $('body').append(
//        '<div class="modal fade" id="AlertDialogId" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">' +
//        '  <div class="modal-dialog" role="document">' +
//        '    <div class="modal-content">' +
//        '      <div class="modal-header ' + type + '">' +
//        '        <h5 class="modal-title">' + title + '</h5 > ' +
//        '        <button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
//        '          <span aria-hidden="true">&times;</span > ' +
//        '        </button>' +
//        '      </div>' +
//        '      <div class="modal-body">' +
//        '        <p>' + message + '</p > ' +
//        '      </div>' +
//        '      <div class="modal-footer">' +
//        '        <button type="button" class="btn btn-accent" data-dismiss="modal">' + actionAfter + '</button > ' +
//        '      </div>' +
//        '    </div>' +
//        '  </div>' +
//        '</div>');

//    $('#AlertDialogId').modal('show');
//    $('#AlertDialogId').on('hidden.bs.modal', function () {
//        $('#AlertDialogId').modal('hide');
//        if (isRedirect)
//            window.location.href = urlRedirect;　// ユーザーマスターへ戻る
//        else
//            location.reload();
//    });

//    $('#exampleModal .btn-accent').click(function () {
//        $('#AlertDialogId').modal('hide');
//        location.reload();
//    });
//}
////--------------------------------------------------------//


////------------------- タイムピッカー ------------------//
//$(document).ready(function () {
//    $.datetimepicker.setLocale('ja');

//    $('#startDate').datetimepicker({
//        format: "Y/m/d",
//        timepicker: false,
//        onShow: function (ct) {
//            this.setOptions({
//                maxDate: jQuery("#end_datetimepicker").val() ? jQuery("#end_datetimepicker").val() : false,
//                formatDate: "Y/m/d"
//            })
//        }
//    });

//    $('#endDate').datetimepicker({
//        format: "Y/m/d",
//        timepicker: false,
//        onShow: function (ct) {
//            this.setOptions({
//                maxDate: jQuery("#end_datetimepicker").val() ? jQuery("#end_datetimepicker").val() : false,
//                formatDate: "Y/m/d"
//            })
//        }
//    });
//});
////--------------------------------------------------------//

////--------------------------------------------------------//
//function onExportExcelWithProductNumber(page) {
//    var startDate = $("#startDate").val();
//    var endDate = $("#endDate").val();
//    var productNumber = $("#ProductNumber").val();
//    showExporting()
//    formData = { startDate: startDate, endDate: endDate, productNumber: productNumber };
//    $.ajax({
//        type: 'POST',
//        url: '' + page + '/ExportExcel',
//        data: formData,
//        success: function (response) {
//            if (response.res == "OK") {
//                var { contentType, fileContents, fileDownloadName } = response.data;
//                {
//                    const link = document.createElement("a");
//                    link.href = `data:${contentType};base64,${fileContents}`;
//                    link.download = fileDownloadName;
//                    link.click();
//                }
//            } else {
//                let html = '<span class="text-danger">' + response.error + '</span>';
//                document.getElementById('ErrorBlock').innerHTML = html;
//            }
//            hideExporting()
//        },
//        error: function (request, status, error) {
//            hideExporting()
//            alert(request.responseText);
//        }
//    });
//}
////--------------------------------------------------------//
