<%@ Page Title="" Language="C#" MasterPageFile="~/GETS.Master" AutoEventWireup="true" CodeBehind="GETS_Contact_JP.aspx.cs" Inherits="GGGETSWeb.GETS_Contact_JP" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
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
	    &nbsp;<a href="#">GETS&nbsp;中国</a>&nbsp;<span class="separator">|</span> <strong>お問い合わせ</strong>

</div>
</div>         
            <div class="pagetitle"><h1>お問い合わせ</h1>
<div class="richtext">ジェ、すべてを包括すると関連する一般的な問題ビジネスコンサルティングから我々のサービスについて。以下のオプションから連絡先を選択します。</div></div>

        </div>
        <div class="container">
            <div class="containerpar parsys">
            <div style="float:left; width:695px;" class="right">
				<div style="margin-top:0px; background-image:url(Images/title2.gif)" class="title">
					<span style="color:#666; font-size:12px;">いつもご利用いただいて、ありがとうございます。下記欄のフォームでご送信ください</span><br/>
					<span style="font-weight:normal; font-size:12px;">&nbsp;&nbsp;&nbsp;(注意：*は必要項目)</span>
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
													ユーザーの利益を保護するためにあなたの本当の名前と連絡先情報を入力して正常にしてください
												</td>
											</tr>
											<tr class="line_h">
												<td width="112" align="right" class="red2" width="120" style="font-size:12px;">
													<strong>お名前</strong>
												</td>
												<td width="290">
													<input type="text" style="border:1px solid #ccc; width:250px;" parm="4" id="realname" name="realname">
												</td>
												<td class="gray" width="200">
													<span id="realname_info" class="note" style="color: #FF0000">*あなたの本当の名前を入力してくださ</span>
												</td>
											</tr>
											
											<tr class="line_h">
												<td width="112" align="right" class="red2" style="font-size:12px;">
													<strong>住所</strong>
												</td>
												<td width="190"> 
													<input type="text" maxlength="18" value="" style="border:1px solid #ccc; width:250px;" parm="5" id="idcard" name="idcard">
													<input type="hidden" id="eIsWallow" name="eIsWallow">
													<input type="hidden" value="1" id="cpaid" name="cpaid">
													<input type="hidden" value="1" id="sourceid" name="sourceid">
													<input type="hidden" value="null" name="IPLibReg">
												</td>
												<td class="yellow">
													<span id="idcard_info" class="note"></span>
												</td>
											</tr>
											<tr>
											<td width="112" align="right" class="red2" style="font-size:12px;">
										    <strong>電話番号</strong>	
											</td>
											<td>
											<input type="text" style="border:1px solid #ccc; width:250px;" parm="4" id="jhcode" name="jhcode">
											</td>
											<td class="gray">
											<span id="Span1" class="note" style="color: #FF0000">*お問い合わせください</span>
											</td>
											</tr>
											<tr>
											<td width="112" align="right" class="red2" style="font-size:12px;">
											<strong>FAX番号</strong>
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
											<strong>郵便番号</strong>
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
											<span id="Span2" class="note" style="color: #FF0000">*正しいメールボックスに記入してください</span>
											</td>
											</tr>
                                            <tr>
											<td width="112" align="right" class="red2" style="font-size:12px;">
											<strong>ご意見</strong>
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
                                                        <input id="Button1" type="button" value="提 出" class=btn />
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

            お問い合わせ

        </a>&nbsp;</h2>


    <div class="ct_image">

        <a title="" href="#">
<img width="70" height="50" class="cq-dd-image shadow_img" title="" alt="" src="Images/Contact/1049_General_70x50.jpg">
        </a>&nbsp;</div>

    <div class="richtext">グループの株式、およびその他の資本市場に関連する問題は、我々の企業のWebサイトを参照してください。<br></div>
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
