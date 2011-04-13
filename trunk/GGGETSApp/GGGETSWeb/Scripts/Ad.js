var js0001_num = 0;
var js0001_t = null; //初始化为空,作为计时器启动和暂停的依据

$(document).ready(function () {
    //JQuery.each(数组,function(){});
    //JQuery.each(数组,function(i,val){});i代表索引,从0开始;val代表该元素；当然此处数组还能是JS数组类型
    $.each($(".picswitch").find("span"), function (i, n) {
        //n代表span标签对象
        $(n).bind("mouseover", function () {
            js0001_choosepic(n); //动态修改图片广告属性
            js0001_num = $(n).attr("id"); //记录目前图片广告的索引
        });
    });

    //整个广告大层绑定鼠标移入事件
    //移入的同时需要暂停计时器
    $(".vouchimg").bind("mouseover", function () {
        if (js0001_t != null) {
            clearTimeout(js0001_t);
            js0001_t = null;
        }
    });

    //整个广告大层绑定鼠标移事出件
    //移出的同时需要启动计时器
    $(".vouchimg").bind("mouseout", function () {
        if (js0001_t == null) js0001_t = setTimeout("js0001_autochanage()", 2000);
    });
    
    js0001_t = setTimeout("js0001_autochanage()", 2000)
});       

//计时器事件
function js0001_autochanage() {
    if (js0001_num == 4) js0001_num = 0;//索引如果走到最后一个，需要重新开始
    else js0001_num++;

    var a = $(".picswitch").find("span").eq(js0001_num);//获取当前计时器自动移动的当前span标签对象
    js0001_choosepic(a); //动态修改图片广告属性
    $(".vouchimg > a > img").css("opacity", "1.0");//不透明
    $(".vouchimg > a > img").animate({ opacity: '1.0' }, 1500);
    js0001_t = setTimeout("js0001_autochanage()", 2000);//递归调用
}

//图片广告title,href和src的动态修改
function js0001_choosepic(a) {
    //parent > child
    $(".picswitch > a > span").attr("class", "unchoose");//将所有span样式设置为'未选中'样式
    $(a).attr("class", "choose");//将当前选中的span样式设置为'选中'样式
    $(".vouchimg > a").eq(0).attr("href", $(a).parent().attr("href")); //遍历vouchimg类样式下所有的a标签，获取第一个(即广告图片)a标签,将它的连接地址修改为当前选中span的父级节点a标签的href地址
    $(".vouchimg > a").eq(0).attr("title", $(a).parent().attr("title")); //遍历vouchimg类样式下所有的a标签，获取第一个(即广告图片)a标签,将它的title修改为当前选中span的父级节点a标签的title
    $(".vouchimg > a > img").attr("src", $(a).attr("picurl")); //遍历vouchimg类样式下所有的a标签，获取第一个(即广告图片)a标签内的img图片,将它的src修改为当前选中span的picurl
};