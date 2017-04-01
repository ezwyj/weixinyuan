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


    var resultTable = $('#result-list').table({
        url: rootUrl + 'Manage/ChangDiSearchList',
        //maxHeight: $('#main').height() - 270,
        data: function () {
            return {
                sq: $('#sqlist').find("option:selected").val(),
                name: $('#name').val()
            };
        },
        paging: {
            pageSize: 10
        },
        tableClass: 'table-condensed',
        colOptions: [{
            name: '序号',
            field: 'Id',
            width: 50,
            align: 'center'
        }, {
            name: '场地',
            field: 'title',
            handler: function (value, data) {
                return '<a href="' + rootUrl + 'Manage/ChangDiDetail?Id=' + data.Id + '" target="_blank">' + data.Title + '</a>';

            }
        }, {
            name: '类型',
            field: 'ClassExp',
            width: 80
        }, {
            name: '可用时间',
            field: 'StartTime',
            width: 80
        }, {
            name: '使用次数',
            field: 'EndTime',
            width: 80
        }, {
            name: '社区',
            field: 'SQExp',
            width: 80
        }, {
            name: '提交日期',
            field: 'InputTimeExp',
            width: 130
        }]
    });

    //////////////////////
    //事件绑定
    //////////////////////


    $('#query').on('click', function () {
        resultTable.table('reload');
    });




    $('#clean').on('click', function () {
        $('input').val('');
        $('#QxState').val('');
        $('#QXDJ').val('');
        $('#QXLB').val('');
    })

});