<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>

<% Option Explicit %>
<HTML xmlns="http://www.w3.org/1999/xhtml">
<HEAD>
<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=utf-8" />
<META NAME="copyright" CONTENT="Copyright 2010-2020 - wygk.cn-STUDIO" />
<META NAME="Author" CONTENT="网软天下源码,www.wygk.cn" />
<META NAME="Keywords" CONTENT="" />
<META NAME="Description" CONTENT="" />
<TITLE>管理员登录</TITLE>
<script language="javascript" src="../Script/Admin.js"></script>
<SCRIPT language=JavaScript src="images/SoftKey.Js"></SCRIPT>

<style type="text/css">
<!--
body {
	margin-left: 0px;
	margin-right: 0px;
}
body,td,th {
	font-size: 12px;
}
.input_login{border:1px #0B7EB8 solid;}
-->
</style></HEAD>

<BODY style="margin-top:100px;font-size:12px;"><BODY 
style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; MARGIN: 0px; OVERFLOW: hidden; BORDER-LEFT: 0px; COLOR: #ffffff; BORDER-BOTTOM: 0px">
<DIV onkeydown=Ctlent()>
<TABLE height="100%" cellSpacing=0 cellPadding=0 width="100%" border=0>
  <TBODY>
  <TR>
    <TD><table 
      style="BORDER-TOP: #0069bf 1px solid; BORDER-BOTTOM: #0069bf 1px solid" 
      height=160 cellspacing=0 cellpadding=0 width="100%" align=center 
        border=0><tbody>
    
          <tr>
            <td align=middle><table height=150 cellspacing=2 cellpadding=0 width="100%" 
            align=center bgcolor=#1890cc border=0>
                <form action="CheckLogin.asp" method="post" name="AdminLogin" id="AdminLogin"  onSubmit="return CheckAdminLogin()">
                  <tbody>
                    <tr>
                      <td align=right width="25%"><img 
                  src="images/Login_pic.gif" align=absMiddle></td>
                      <td width="2%"><img src="images/Login_line.gif" 
                  align=absMiddle></td>
                      <td width="73%"><table cellspacing=2 cellpadding=0 border=0>
                          <tbody>
                            <tr>
                              <td colspan=7><img src="images/Login_tit.gif" 
                        align=absMiddle></td>
                              <td valign=bottom align=right width=90 rowspan=2><img 
                        style="CURSOR: pointer" onClick=CheckForm(); height=71 
                        src="images/Login.gif" width=71 align=absMiddle 
                        border=0></td>
                            </tr>
                            <tr>
                              <td align=right width=50 height=60><img 
                        src="images/admin_login_01.gif" align=absMiddle></td>
                              <td>用户名<br>
                                  <input class=input_login 
                        style="WIDTH: 80px; HEIGHT: 18px" size=12 
                      name=LoginName></td>
                              <td align=right width=40><img 
                        src="images/admin_login_02.gif" align=absMiddle></td>
                              <td>密　码
                                <table class=input_login cellspacing=0 cellpadding=0 
                        border=0>
                                    <tbody>
                                      <tr>
                                        <td bgcolor=#ffffff><input 
                              style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; WIDTH: 60px; BORDER-BOTTOM: 0px; HEIGHT: 14px" 
                              type=password name=LoginPassword></td>
                                        <td width=18 bgcolor=#ffffff><img 
                              src="images/Dot_Info.gif" width="17" height="14" 
                              border=0 align=absMiddle 
                              style="CURSOR: pointer" 
                              title=点此使用小键盘输入密码，使你的帐号信息更安全！ 
                              onClick="inputpass=login.Password;showkeyboard();login.Password.readOnly=1;Calc.Password.value=''" 
                              onKeyDown=Calc.Password.value=login.Password.value 
                              onchange="Calc.passcnbbr.value=login.passcnbbr.value" 
                              readonly></td>
                                      </tr>
                                    </tbody>
                                </table></td>
                              <td align=right width=45><img 
                        src="images/admin_login_03.gif" align=absMiddle></td>
                              <td>验证码<br>
                                  <input class=input_login 
                        style="WIDTH: 50px; HEIGHT: 18px" maxlength=6 size=8 
                        name=Code>
                                  <img src="../Include/VerifyCode.asp" align="absmiddle"></td>
                              <td>&nbsp;</td>
                            </tr>
                          </tbody>
                      </table></td>
                    </tr>
                </form>
              
              </table></td>
          </tr>
      后台管理只允许一人登陆，当有其他访问者进入时你将自动退出，如果你还想继续观看，可以再次登陆

      </table>    </TD>
  </TR></TBODY></TABLE>
</DIV>
</BODY>
</HTML>














































































































