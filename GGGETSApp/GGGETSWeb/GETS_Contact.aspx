﻿<%@ Page Title="" Language="C#" MasterPageFile="~/GETS.Master" AutoEventWireup="true" CodeBehind="GETS_Contact.aspx.cs" Inherits="GGGETSWeb.GETS_Contact" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/global.css" rel="stylesheet" type="text/css" />
    <link href="Styles/css.css" rel="stylesheet" type="text/css" />
    <link href="Styles/defalut.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Contact.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_area">
<div class="navigation_content">
    
    <div class="headlineflashimage parbase">
		<div id="flash_teaser_small" style="MARGIN-BOTTOM: -4px">
		
		<a alt="" title="" target="" href="#">
			
<img width="200" height="120" class="cq-dd-flash" title="" alt="" src="Images/Contact/image.jpg">
        </a>   
		</div>
</div> 
    <div class="leftmenunav">
<a id="navigation_content"></a>
</div>
    <div class="favoritesselector">
		<div class="startpageSelect" id="startpageSelect">
		</div>
</div>
</div>
<div class="content_main_container">
    <a id="content_main"></a>
    
    <!-- if index page without yellow header, use content_main_index -->
    <div class="content_main">
        <div class="container_title_main">
            <div class="breadcrumb">
	<div class="breadcrumb" style="">
	    &nbsp;<a href="#">GETS&nbsp;中国</a>&nbsp;<span class="separator">|</span> <strong>联系我们</strong>

</div>
</div>         
            <div class="pagetitle"><h1>联系我们</h1>
<div class="richtext">从关于我们服务的业务咨询到与GETS相关的一般问题，无所不包。请从下方选择联络选项。</div></div>

        </div>
        <div class="container">
            <div class="containerpar parsys">
            <div style="float:left; width:695px;" class="right">
				<div style="margin-top:0px; background-image:url(Images/title2.gif)" class="title">
					<span style="color:#666; font-size:12px;">感谢您对我们的支持,请在下面留下您的名字和您对我们的意见和建议</span><br/>
					<span style="font-weight:normal; font-size:12px;">&nbsp;&nbsp;&nbsp;(注：带星号的为必填项)</span>
				</div>
				<div style=" border:1px solid #efefef;" class="padding">
					<form action="#" style="padding: 0;margin: 0;" method="post" name="regform" id="regform">
						<table cellspacing="6" cellpadding="0" border="0" align="center" class="line_h">
							<tbody>
							<tr>
								<td align="left" colspan="3">
									<div style="border:1px solid #dbdbdb; background:#FEF7EB;">
										<table width="100%" cellspacing="6" cellpadding="0" border="0">
											<tbody><tr class="line_h">
												<td align="center" class="red2" colspan="3" style="font-size:12px;">
													请正确填写您的真实姓名及联系方式以保障您的用户权益
												</td>
											</tr>
											<tr class="line_h">
												<td width="112" align="right" class="red2" width="120" style="font-size:12px;">
													<strong>真实姓名</strong>
												</td>
												<td width="290">
													<input type="text" style="border:1px solid #ccc; width:250px;" parm="4" id="realname" name="realname">
												</td>
												<td class="gray" width="200">
													<span id="realname_info" class="note" style="color: #FF0000">*请填写您的真实姓名</span>
												</td>
											</tr>
											
											<tr class="line_h">
												<td width="112" align="right" class="red2" style="font-size:12px;">
													<strong>联系地址</strong>
												</td>
												<td width="190"> 
													<input type="text" maxlength="18" value="" style="border:1px solid #ccc; width:250px;" parm="5" id="idcard" name="idcard">
												</td>
												<td class="yellow">
													<span id="idcard_info" class="note"></span>
												</td>
											</tr>
											<tr>
											<td width="112" align="right" class="red2" style="font-size:12px;">
										    <strong>联系电话</strong>	
											</td>
											<td>
											<input type="text" style="border:1px solid #ccc; width:250px;" parm="4" id="jhcode" name="jhcode">
											</td>
											<td class="gray">
											<span id="Span1" class="note" style="color: #FF0000">*请填写联系电话</span>
											</td>
											</tr>
											<tr>
											<td width="112" align="right" class="red2" style="font-size:12px;">
											<strong>传真</strong>
											</td>
											<td>
											
											<input type="text" style="border:1px solid #ccc; width:250px;" parm="4" 
                                                    id="jhcode0" name="jhcode0">
																						
											</td>
											<td class="gray">
											
											</td>
											</tr>
											<tr>
											<td width="112" align="right" class="red2" style="font-size:12px;">
											<strong>邮编</strong>
											</td>
											<td>
											
											<input type="text" style="border:1px solid #ccc; width:250px;" parm="4" 
                                                    id="Text1" name="jhcode0">
																						
											</td>
											<td class="gray">
											
											</td>
											</tr>
                                            <tr>
											<td width="112" align="right" class="red2" style="font-size:12px;">
											<strong>E-mail</strong>
											</td>
											<td>
											
											<input type="text" style="border:1px solid #ccc; width:250px;" parm="4" 
                                                    id="Text2" name="jhcode0">
																						
											</td>
											<td class="gray">
											<span id="Span2" class="note" style="color: #FF0000">*请填写正确的邮箱</span>
											</td>
											</tr>
                                            <tr>
											<td width="112" align="right" class="red2" style="font-size:12px;">
											<strong>留言内容</strong>
											</td>
											<td>
											
                                            <textarea style="border:1px solid #ccc; width:250px;" parm="4" 
                                                    id="Text3" name="jhcode0" cols="20" rows="3"></textarea>
																						
											</td>
											<td class="gray">
											
											</td>
											</tr>
										</tbody></table>
									</div>
								</td>
							</tr>

							<tr>
								<td colspan="3" align="center">
									<table cellspacing="0" cellpadding="0" border="0" align="center">
										<tbody><tr>
											<td>
												<div id="login_in_btn">
													
													<a id="submitbutton" href="#">
                                                        <input id="Button1" type="button" value="提 交" class=btn />
												</div>
												
											</td>
										</tr>
									</tbody></table>
								</td>
							</tr>
						</tbody></table>
					</form>
				</div>
			</div>
            </div>
        </div>
        <div class="clearAll">&nbsp;</div>       
        <div class="clearAll">&nbsp;</div>
    </div>
    <div class="content_cross_reference">	
		    <div class="crossrefpar parsys">
<a style="visibility:hidden" name="crossrefpar_referenceparagraph"></a><div class="section reference parbase referenceparagraph">
          	  <div class="cq-dd-paragraph" style="display:inline;"><div class="contentteaser standardarticle">
<!-- start contentteaser -->
	<div class="module dhl content_teaser">
<div>
    <h2>
        <a title="" href="">

            联络

        </a>&nbsp;</h2>


    <div class="ct_image">

        <a title="" href="#">
<img width="70" height="50" class="cq-dd-image shadow_img" title="" alt="" src="Images/Contact/1049_General_70x50.jpg">
        </a>&nbsp;</div>

    <div class="richtext">有关本集团股份以及其它资本市场相关的问题，请登录我们的企业网站。<br></div>
</div>
<div style="clear:both"></div>
</div>
</div>
</div></div>
</div>   
    </div>
</div>
    </div>
</asp:Content>
