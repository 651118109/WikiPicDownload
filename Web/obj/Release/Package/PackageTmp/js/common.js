function ajaxPost(url, paras) {
    $.post("/Actions/Download.ashx", paras, function (obj) {
        alert(obj.status);
    }, "json");
}

//function post(url, paras) {
//    var temp = document.createElement("form");
//    temp.action = url;
//    temp.method = "post";
//    temp.style.display = "none";
//    for (var x in paras) {
//        var opt = document.createElement("textarea");
//        opt.name = x;
//        opt.value = paras[x];
//        // alert(opt.name)
//        temp.appendChild(opt);
//    }
//    document.body.appendChild(temp);
//    temp.submit();
//    return temp;
//}
function post(url, paras) {
    var form = $("<form method='post'></form>");
    form.attr({ "action": url });
    for (arg in paras) {
        var input = $("<input type='hidden'>");
        input.attr({ "name": arg });
        input.val(paras[arg]);
        form.append(input);
    }
    $(document.body).append(form);//chrome 56版后安全策略更新，你需要把你的form给插进dom元素中才能submit。
    form.submit();
}