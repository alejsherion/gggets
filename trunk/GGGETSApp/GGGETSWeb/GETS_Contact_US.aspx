<%@ Page Title="" Language="C#" MasterPageFile="~/GETS.Master" AutoEventWireup="true" CodeBehind="GETS_Contact_US.aspx.cs" Inherits="GGGETSWeb.GETS_Contact_US" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"  %>
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
	    &nbsp;<a href="#">GETS&nbsp;USA</a>&nbsp;<span class="separator">|</span> <strong>CONTACT US</strong>

</div>
</div>         
            <div class="pagetitle"><h1>CONTACT US</h1>
<div class="richtext">About our services from business consulting to general issues related with GETS, all-encompassing. Select a contact from the options below.</div></div>

        </div>
        <div class="container">
            <div class="containerpar parsys">
            <div style="float:left; width:695px;" class="right">
				<div style="margin-top:0px; background-image:url(Images/title2.gif)" class="title">
					<span style="color:#666; font-size:12px;">Thank you for your support, please leave your name below and your comments and suggestions to us</span><br/>
					<span style="font-weight:normal; font-size:12px;">&nbsp;&nbsp;&nbsp;(Note: with an asterisk are required)</span>
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
													Please correctly fill in your real name and contact information to protect the interests of your users
												</td>
											</tr>
											<tr class="line_h">
												<td width="112" align="right" class="red2" width="120" style="font-size:12px;">
													<strong>Real Name</strong>
												</td>
												<td width="290">
													<input type="text" style="border:1px solid #ccc; width:250px;" parm="4" id="realname" name="realname">
												</td>
												<td class="gray" width="200">
													<span id="realname_info" class="note" style="color: #FF0000">*Please fill in your real name</span>
												</td>
											</tr>
											
											<tr class="line_h">
												<td width="112" align="right" class="red2" style="font-size:12px;">
													<strong>Contact Address</strong>
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
										    <strong>Contact Phone</strong>	
											</td>
											<td>
											<input type="text" style="border:1px solid #ccc; width:250px;" parm="4" id="jhcode" name="jhcode">
											</td>
											<td class="gray">
											<span id="Span1" class="note" style="color: #FF0000">*Please fill in your contact phone</span>
											</td>
											</tr>
											<tr>
											<td width="112" align="right" class="red2" style="font-size:12px;">
											<strong>Fax</strong>
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
											<strong>Postcode</strong>
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
											<span id="Span2" class="note" style="color: #FF0000">*Please fill in your E-mail</span>
											</td>
											</tr>
                                            <tr>
											<td width="112" align="right" class="red2" style="font-size:12px;">
											<strong>Message</strong>
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
                                                        <input id="Button1" type="button" value="submit" class=btn />
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

            Contact

        </a>&nbsp;</h2>


    <div class="ct_image">

        <a title="" href="#">
<img width="70" height="50" class="cq-dd-image shadow_img" title="" alt="" src="Images/Contact/1049_General_70x50.jpg">
        </a>&nbsp;</div>

    <div class="richtext">Shares of the Group, and other capital market related issues, please visit our corporate website.<br></div>
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
