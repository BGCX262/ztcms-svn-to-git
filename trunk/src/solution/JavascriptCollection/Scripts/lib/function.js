/* Function */
//加入收藏
function AddFavorite(sUrl, sTitle) {
    try {
        window.external.addFavorite(sUrl, sTitle);
    } catch (e) {
        try {
            window.sidebar.addPanel(sTitle, sUrl, "");
        } catch (e) {
            alert("加入收藏失败，请使用Ctrl+D进行添加");
        }
    }
}

//设为首页
function SetHome(obj, vrl) {
    try {
        obj.style.behavior = 'url(#default#homepage)';
        obj.setHomePage(vrl);
    } catch (e) {
        if (window.netscape) {
            try {
                window.netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            } catch (e) {
                alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入“about:config”并回车\n然后将 [signed.applets.codebase_principal_support]的值设置为'true',双击即可。");
            }
            var prefs = window.Components.classes['@mozilla.org/preferences-service;1'].getService(window.Components.interfaces.nsIPrefBranch);
            prefs.setCharPref('browser.startup.homepage', vrl);
        }
    }
}

//限制用户只输入数字  examle:  onkeypress="return typeOnlyDigital(event)"
function typeOnlyDigital(event) {
    var charCode = (typeof event.which == 'number') ? event.which : event.keyCode;
}
