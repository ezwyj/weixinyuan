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


        return XinYuanModel;

    }
   

    //////////////////////
    //事件绑定
    //////////////////////


    $('#submitxinyuan').on('click', function () {
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
            name: "",
            telephone: "",
            weixinOpenId : ""
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