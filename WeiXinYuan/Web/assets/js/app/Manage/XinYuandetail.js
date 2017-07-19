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




    function GetModel() {
       
        XinYuanModel.Id = $('#id').val();
        XinYuanModel.Title = $('#title').val();
        XinYuanModel.Name = $('#name').val();
        XinYuanModel.SQ = $('#sqlist').find("option:selected").val();
        XinYuanModel.LY = $('#ly').val();
        XinYuanModel.Status = $('#statuslist').find("option:selected").val();
        XinYuanModel.Telephone = $('#telephone').val();
        XinYuanModel.InputTime = $('#inputtime').val();
        XinYuanModel.Number = $('#number').val();

        return XinYuanModel;

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
                    window.location.href = rootUrl + 'Manage/XinYuanIndex';
                });



            }
        });

    });

    
   
    $('#changeState').on('click', function (e) {

        $.confirm('确定要发放吗？', function (result) {
            if (result) {
                $.loading('提交中，请稍后...');
                upModelData = GetModel();
                $.post(rootUrl + 'Manage/ChangeXinYuanRenLingState', {
                    id: e.target.getAttribute('data-data')
                }, function (res) {
                    $.tlayer('close');
                    if (!res) {
                        $.tips('认领失败', 1);
                    }
                    else {
                        $.tips('认领成功', 3);

                    }
                    window.location.href = rootUrl + 'Manage/XinYuanIndex';
                });



            }
        });

    });
    

});