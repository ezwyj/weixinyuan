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
        XinYuanModel.Status = $('#statuslist').find("option:selected").val();
        XinYuanModel.Telephone = $('#telephone').val();
        XinYuanModel.InputTime = $('#inputtime').val();
        XinYuanModel.InputTime = $('#inputtime').val();


        return XinYuanModel;

    }
   

    //////////////////////
    //事件绑定
    //////////////////////


    $('#submitxinyuan').on('click', function () {
        $.confirm('确定要保存吗？', function (result) {
            if (result) {
                $.loading('提交中，请稍后...');
                upModelData = GetModel();
                $.post(rootUrl + 'Manage/XinYuanDetail', {
                    dataJson: JSON.stringify(upModelData)
                }, function (res) {
                    $.tlayer('close');
                    if (!res) {
                        $.tips('保存失败', 1);
                    }
                    else {
                        $.tips('保存成功', 3);

                    }
                    window.location.href = rootUrl + 'Miblie/Index';
                });



            }
        });
    });





});