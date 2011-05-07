<%@ Page Title="" Language="C#" MasterPageFile="~/GETS.Master" AutoEventWireup="true" CodeBehind="GETS_Main_JP.aspx.cs" Inherits="GGGETSWeb.GETS_Main_JP" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/panning-slideshow.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/Scripts/jquery-1.4.1.min.js" />
        <asp:ScriptReference Path="~/Scripts/jquery.easing.1.3.js" />
        <asp:ScriptReference Path="~/Scripts/jquery.timer.js" />
        <asp:ScriptReference Path="~/Scripts/image-rotator.js" />
    </Scripts>
    </asp:ScriptManager>
<!--main content-->
<div class="content_main">
                   
                    <div class="content_left">
                        <%--<div class="container_title">--%>
                           <%-- <div class="headlineflashimage parbase">
                                <div id="headlineflashimage_placeholder">
                                    <div id="mainFlash" style="width: 380px; height: 245px;">--%>
                                    <div id="window" style="width: 380px; height: 245px;">
                                    <%--<div class="vouchimg relative">
			                            <a title="广告名01" target="_blank" id="picswitch" href="#"><img src="Images/ad/mainAd.jpg" alt="" /></a>
			                            <div class="picswitch absolute" style="display:none;">
                                            <!-- a标签的title和href需要动态绑定 -->
			                                <a title="广告名01" target="_blank" href="#"><span id="0" picurl="Images/ad/mainAd.jpg" class="choose">广告名01</span></a>
                                            <a title="广告名02" target="_blank" href="#"><span id="1" picurl="Images/ad/mainAd2.jpg" class="unchoose">广告名02</span></a>
                                            <a title="广告名03" target="_blank" href="#"><span id="2" picurl="Images/ad/mainAd3.jpg" class="unchoose">广告名03</span></a>
                                            <a title="广告名04" target="_blank" href="#"><span id="3" picurl="Images/ad/mainAd4.jpg" class="unchoose">广告名04</span></a>
                                            <a title="广告名02" target="_blank" href="#"><span id="4" picurl="Images/ad/mainAd5.jpg" class="unchoose">广告名05</span></a>
		                                </div>
                                    </div>--%>
                                    <ul id="slideshow">
			                            <li class="box1"><img alt="WELCOME GETS" src="Images/ad/mainAd.jpg" /></li>
			                            <li class="box2"><img alt="WELCOME GETS" src="Images/ad/mainAd3.jpg" /></li>
			                            <li class="box3"><img alt="WELCOME GETS" src="Images/ad/mainAd4.jpg" /></li>
			                            <li class="box4"><img alt="WELCOME GETS" src="Images/ad/mainAd5.jpg" /></li>
		                            </ul>
                                    </div>
                         <%--           </div>
                                </div>
                            </div>--%>
                     <%--   </div>--%>
                        <div class="contentleftpar parsys">
                            <a name="contentleftpar_contentteaser" style="visibility: hidden;"></a>
                            <div class="contentteaser section standardarticle">
                                <!-- start contentteaser -->
                                <div class="module dhl content_teaser">
                                    <div style="clear: both;">
                                    </div>
                                </div>
                            </div>
                            <a name="contentleftpar_codeinclusion" style="visibility: hidden;"></a><a name="contentleftpar_referenceparagraph"
                                style="visibility: hidden;"></a>
                            <div class="section reference parbase referenceparagraph">
                                <div style="display: inline;" class="cq-dd-paragraph">
                                    <div class="contentteaser standardarticle">
                                        <!-- start contentteaser -->
                                        <div class="module dhl content_teaser">
                                            <div>
                                                <h2>
                                                    公司新闻</h2>
                                                <div class="ct_image">
                                                    <a href="http://www.cn.dhl.com/zh/logistics/industry_sector_solutions.html" title=""
                                                        target="_self">
                                                        <img src="Images/1000_desc_70x50.jpg" alt="" title="" class="cq-dd-image shadow_img"
                                                            height="50" width="70">
                                                    </a>
                                                </div>
                                                <div class="richtext">
                                                    GETS对所选行业的专注，代表着客户能受惠于与我们的专家合作；所涉领域不仅包括物流，亦涵盖客户所处的特定市场。凭借我们行业领先的解决方案，提供客户真正的竞争优势。<br>
                                                   </div>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class ="content_right">
                    <div class="module dhl">
    <div class="fast_track_container">
        <div class="wrap1">
            <div class="wrap2">
                <div class="wrap3">
                    <div class="wrap4">
                        <div class="wrap5">
                            <div class="wrap6">
                                <div class="wrap7">
                                    <div class="wrap8">
                                        <div class="fast_track">
                                        
                                        
                                           	<div class="fast_track_headline">&nbsp;国际运单查询</div>
                                            <form onsubmit="this.elements['AWB_containerleftpar_fasttrack'].value = (this.elements['AWB_containerleftpar_fasttrack'].value=='Enter your tracking number(s)')?'':this.elements['AWB_containerleftpar_fasttrack'].value;" action="/content/g0/en/express/tracking.shtml" method="get" id="trackingIndex_fast_containerleftpar_fasttrack" name="trackingIndex_fast_containerleftpar_fasttrack">
                                                <fieldset>
                                                <legend>Tracking</legend>
	                                         
                                                <label class="no_label" for="AWB_containerleftpar_fasttrack">Tracking code</label>
                                                
                                                
                                                	<textarea onkeypress="fnFTCheckEntercontainerleftpar_fasttrack(event,'containerleftpar_fasttrack',this);" onblur="this.value=(fnFTstrip(this.value)=='')?'Enter your tracking number(s)':this.value;" onfocus="this.value=(this.value==fnFTstrip('Enter your tracking number(s)'))?'':this.value;" wrap="soft" class="code" id="AWB_containerleftpar_fasttrack" rows="3" cols="30" name="AWB"></textarea>
       								
                                                <input type="button" onclick="javascript:fnCheckExtURLcontainerleftpar_fasttrack(this,'containerleftpar_fasttrack');" value="查询" class="track" id="trackbut_containerleftpar_fasttrack">
                            
                                                </fieldset>
                                            </form>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
         </div>
        <div class="clearAll"></div>
    </div>
</div>
<div class="contentteaser section standardarticle">

<!-- start contentteaser -->
	<div class="module dhl content_teaser">

<div>
    <h2>
       重要通告
    </h2>


    <div class="ct_image">

       
<img width="70" height="50" class="cq-dd-image shadow_img" title="" alt="" src="Images/information_key_70x50px.png">
        

    </div>

    <div class="richtext">服务公告使您随时了解GETS业务的事件<br><a href="#" class="arrowLink">东京电力系统恢复工作</a><br></div>

</div>
<div style="clear: both;"></div>

</div>
</div>
                    </div>
                    <div class="clearAll">
                        &nbsp;</div>
                </div>
<!--login-->
<div class="content_cross_reference">
<div class="crossrefpar parsys">
<a style="visibility: hidden;" name="crossrefpar_referenceparagraph_4"></a><div class="section reference parbase referenceparagraph"></div>
<a style="visibility: hidden;" name="crossrefpar_referenceparagraph_9"></a><div class="section reference parbase referenceparagraph">
<div class="cq-dd-paragraph" style="display: inline;"><div class="transactionteaser">
<div class="transactionteaser_white module dhl">
<div class="wrap1">
<div class="wrap2">
<div class="wrap3">
<div class="wrap4">
    <div class="wrap5">
        <div class="wrap6">
            <div class="wrap7">
                <div class="wrap8">
                    <div class="transaction_teaser_white">
                        <div class="transaction_teaser_headline">

											

                        </div>
						<div class="transparsys parsys">
<a style="visibility: hidden;" name="containerleftpar_transactionteaser_transparsys_contentteaser"></a><div class="contentteaser section standardarticle">

<!-- start contentteaser -->
<div class="module dhl content_teaser">

<div>

<div class="ct_image img_large">
<img width="220" height="100" class="cq-dd-image shadow_img" title="" alt="" src="Images/Client.jpg">
</div>


</div>
<div style="clear: both;"></div>

</div>
</div>
<a style="visibility: hidden;" name="containerleftpar_transactionteaser_transparsys_countryselector_9a67"></a><div class="section countryselector">


<div class="module">

<div class="link_list_white">

<h2>


</h2>
<form onsubmit="goTo(this.elements['country'].options[this.elements['country'].selectedIndex].value); return false;" method="get" action="#" id="countrylistform_containerleftpar_transactionteaser_transparsys_countryselector_9a67" name="countrylistform_containerleftpar_transactionteaser_transparsys_countryselector_9a67">
<fieldset>
<legend> </legend>
<p> </p>
<!-- Added for DHL-1941 -->
<div class="error" id="countryselector_containerleftpar_transactionteaser_transparsys_countryselector_9a67" style="display: none;">
</div>
        
<label for="worldwide">用户名</label>&nbsp;&nbsp;&nbsp;
<input />
<br/> 
<br/>      
<label for="country">密&nbsp;&nbsp;&nbsp;&nbsp;码</label>&nbsp;&nbsp;&nbsp;
<input />
                      
<input type="submit" class="go" onclick="javascript:return CheckCountrySelector(&quot;countrylistform_containerleftpar_transactionteaser_transparsys_countryselector_9a67&quot; ,&quot;Choose a location&quot;,&quot;true&quot;,&quot;g0&quot;, &quot;countryselector_containerleftpar_transactionteaser_transparsys_countryselector_9a67&quot;)" id="" value="Go">
<div class="clearAll"></div>
</fieldset>
</form>
</div>
</div>

<!-- Added for DHL-1941 -->
<script type="text/javascript">
    /* &lt;![CDATA[ */
    var errorMessage = "Please select a country.";
    /* ]]&gt; */
</script></div>

</div>

       								
        								
	                </div>
                </div>
            </div>
        </div>
    </div>
</div>
</div>
</div>
</div>
<div class="clearAll"></div>
</div></div>
</div></div>
<a style="visibility: hidden;" name="crossrefpar_referenceparagraph_0"></a>
<a style="visibility: hidden;" name="crossrefpar_componentseparator"></a><div class="section ghost">
</div>
<a style="visibility: hidden;" name="crossrefpar_referenceparagraph_1"></a>
<a style="visibility: hidden;" name="crossrefpar_componentseparator_0"></a>
<a style="visibility: hidden;" name="crossrefpar_referenceparagraph_2"></a>
<a style="visibility: hidden;" name="crossrefpar_componentseparator_1"></a>
<a style="visibility: hidden;" name="crossrefpar_referenceparagraph"></a>

</div>
</div>
</asp:Content>
