require(['common', 'util', 'template', 'plugins', 'uploadify', 'datepicker', 'bootstrap'], function ($, util, template) {
    var rootUrl = OP_CONFIG.rootUrl;
    var Id = $('[name="Id"]').val();
    var operater = $('[name="operater"]').val();
    var operaterName = $('[name="operaterName"]').val();
    var stage = $('[name="stage"]').val();

    //////////////////////
    //页面初始化
    //////////////////////






    $(document).ready(function () {

    });



    //上传附件
    $('#select-attachment').uploadify({
        swf: rootUrl + 'assets/js/lib/uploadify.swf',
        uploader: rootUrl + 'Common/UploadAttachment',
        fileSizeLimit: '10 MB',

        onSelect: function (file) {

            if (file.type == '.exe' || file.type == '.bat' || file.type == '.dll') {
                $.tips('限制类文件，请重新上传！', 0);
                return false;
            }


            var queue = $('#' + this.settings.queueID);
            var html = '<div id="#{fileId}" class="uploadify-queue-item">' +
                            '<span class="icon #{fileIcon}"></span>' +
                            '<span class="file-name" title="#{fileName}">#{fileName}</span>' +
                            '<div class="uploadify-progress">' +
                                '<div class="uploadify-progress-bar">&nbsp;</div>' +
                            '</div>' +
                            '<span class="data">Waiting</span>' +
                        '</div>';

            var fileData = {
                fileId: file.id,
                fileName: file.name,
                fileIcon: util.getFileIcon(file.name)
            }


            queue.append(util.parseTpl(html, fileData));
        },
        onUploadSuccess: function (file, res) {
            res = JSON.parse(res);

            if (res.state) {
                var data = res.data[0];
                var html = '<div id="#{fileId}" class="finish-queue-item" data-fileid="#{id}">' +
                                '<span class="icon #{fileIcon}"></span>' +
                                '<span class="file-name" title="#{fileName}">#{fileName}</span>' +
                                '<span class="file-size">#{fileSize}</span>' +
                                '<a class="file-operate file-del" href="#" id="DelFile_#{id}">删除</a>' +
                            '</div>';
                var fileData = {
                    id: data.Id,
                    fileId: file.id,
                    fileIcon: util.getFileIcon(file.name),
                    fileName: file.name,
                    fileSize: util.getFileSize(file.size)
                }

                $('#' + file.id).replaceWith(util.parseTpl(html, fileData));

                //删除
                $('#' + file.id).on('click', '.file-del', function () {
                    $('#' + file.id).remove();
                    return false;
                });
            } else {
                $.tips(res.msg);
            }
        },
        onUploadStart: function (file) {
            $("#select-attachment").uploadify("settings", "formData", {  });
            //在onUploadStart事件中，也就是上传之前，把参数写好传递到后台。  
        }
    });

    function GetModel() {

        HuoDongModel.Id = $('#id').val();
        HuoDongModel.Title = $('#title').val();
        HuoDongModel.InputName = $('#name').val();
        HuoDongModel.SQ = $('#sqlist').find("option:selected").val();
        HuoDongModel.Content = $('#Content').val();
        HuoDongModel.Status = $('#statuslist').find("option:selected").val();
        HuoDongModel.InputTime = $('#inputtime').val();
        HuoDongModel.Max = $('#max').val();
        HuoDongModel.Address = $('#address').val();
        HuoDongModel.StartTime = $('#starttime').val();
        HuoDongModel.EndTime = $('#endtime').val();

        var attachments = [];
        var panel = $('.panel-body');
        panel.find('.finish-queue-item').each(function () {
            if ($(this).data('fileid')) {
                attachments.push($(this).data('fileid'));
            }
        });

        HuoDongModel.Image = attachments.join();

        return HuoDongModel;

    }

    $('.datepicker').datepicker({
        format: 'yyyy/m/d',
        autoclose: true,
        clearBtn: true,
        todayHighlight: true,
        language: 'zh-CN'
    });

    //////////////////////
    //事件绑定
    //////////////////////


    $('#save').on('click', function () {
        $.confirm('确定要保存吗？', function (result) {
            if (result) {
                $.loading('提交中，请稍后...');
                upModelData = GetModel();
                $.post(rootUrl + 'Manage/HuoDongDetail', {
                    dataJson: JSON.stringify(upModelData)
                }, function (res) {
                    $.tlayer('close');
                    if (!res) {
                        $.tips('保存失败', 1);
                    }
                    else {
                        $.tips('保存成功', 3);

                    }
                    window.location.href = rootUrl + 'Manage/HuoDongIndex';
                });



            }
        });

    });






});