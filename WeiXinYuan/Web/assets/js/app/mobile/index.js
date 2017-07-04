require(['common', 'util', 'plugins', 'bootstrap', 'datepicker'], function ($, util) {
    var rootUrl = OP_CONFIG.rootUrl;

    //////////////////////
    //页面初始化
    //////////////////////

    $('.datepicker').datepicker({
        format: 'yyyy/m/d',
        autoclose: true,
        clearBtn: true,
        todayHighlight: true,
        language: 'zh-CN'
    });

    function GetModel() {

        XinYuanModel.Id = $('#id').val();
        XinYuanModel.Title = $('#title').val();
        XinYuanModel.Name = $('#name').val();
        XinYuanModel.SQ = $('#sqlist').find("option:selected").val();
        XinYuanModel.LY = $('#ly').val();
        XinYuanModel.Telephone = $('#telephone').val();
        XinYuanModel.Image = $('#imgCardFront').attr('title');

        return XinYuanModel;

    }

    function GetChangDiModel() {

        ChangDiModel.Id = $('#id').val();
        ChangDiModel.Title = $('#title').val();
        ChangDiModel.Name = $('#name').val();
        ChangDiModel.SQ = $('#sqlist').find("option:selected").val();
        ChangDiModel.Device = $('#device').val();
        ChangDiModel.Telephone = $('#telephone').val();
        ChangDiModel.Address = $('#address').val();
        ChangDiModel.Content = $('#content').val();
        ChangDiModel.Max = $('#max').val();
        ChangDiModel.Money = $('#money').val();
        return ChangDiModel;

    }
   
    var syncUpload = function (localIds, targetInput) {
        var localId = localIds.pop();


        wx.uploadImage({
            localId: localId,
            isShowProgressTips: 1,
            success: function (res) {
                var serverId = res.serverId; // 返回图片的服务器端ID
                $("#" + targetInput).attr('title', serverId);
                //其他对serverId做处理的代码
                //if (localIds.length > 0) {
                //    syncUpload(localIds);
                //}
            }
        });
    };
    //////////////////////
    //事件绑定
    //////////////////////
    $('#changdisubmit').on('click', function () {
        upModelData = GetChangDiModel();
        $.post(rootUrl + 'Manage/ChangDiDetail', {
            dataJson: JSON.stringify(upModelData)
        }, function (res) {
           
            if (!res) {
                alert('保存失败');
            }
            else {
                alert('保存成功');

            }
            window.location.href = rootUrl + 'Mobile/Index';
        });
    });

    $('#submitxinyuan').on('click', function () {
        ///提交心愿
        upModelData = GetModel();
        $.post(rootUrl + 'Manage/XinYuanDetail', {
            dataJson: JSON.stringify(upModelData)
        }, function (res) {
           
            if (!res) {
                alert('保存失败');
            }
            else {
                alert('保存成功');

            }
            window.location.href = rootUrl + 'Mobile/Index';
        });
    });

    $('#xinyuanrenling').on('click', function () {

        $.post(rootUrl + 'Mobile/XinYuanRenLing', {
            xinyuanid: $('#id').val(),
            name: $('#rlname').val(),
            telephone: $('#rltele').val(),
            weixinOpenId: ''
        }, function (res) {

            if (!res) {
                alert('保存失败');
            }
            else {
                alert('保存成功');

            }
            window.location.href = rootUrl + 'Mobile/Index';
        });
    });

    $('#actionhuodong').on('click', function () {

        $.post(rootUrl + 'Mobile/HuoDonggz', {
            huodongid: $('#huodongid').val(),
            name: "",
            telephone: "",
            weixinOpenId: ""
        }, function (res) {

            if (!res) {
                alert('保存失败');
            }
            else {
                alert('保存成功');

            }
            window.location.href = rootUrl + 'Mobile/Index';
        });
    });

    $('#selectPicture').on('click', function () {

        wx.chooseImage({
            success: function (res) {
                var localIds = res.localIds;
                $("#imgCardFront").attr('src', localIds[0]);
                syncUpload(localIds, 'imgCardFront');
            }
        });
    });

        $('#addhuodong').on('click', function () {

            $.post(rootUrl + 'Mobile/HuoDongAdd', {
                huodongid: $('#huodongid').val(),
                name: "",
                telephone: "",
                weixinOpenId: ""
            }, function (res) {

                if (!res) {
                    alert('保存失败');
                }
                else {
                    alert('保存成功');

                }
                window.location.href = rootUrl + 'Mobile/Index';
            });
        });

    });

    
