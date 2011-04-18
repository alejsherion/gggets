<%@ Control Language="C#" AutoEventWireup="true" Inherits="SWFUpload_UC_SWFUpload" Codebehind="UC_SWFUpload.ascx.cs" %>
<script type="text/javascript">
    var <%= this.GetControlId("id") %>={SWFObj:new Object()}
    function <%= this.GetControlId("load") %>() {
        var LoadSettings = {
            post_params:{
                            ASPSESSID: "<%=Session.SessionID %>",
		                    path: "<%=SwfUploadInfo.Path%>",
		                    fn:"<%=SwfUploadInfo.OldFileName%>",
		                    small:"<%=SwfUploadInfo.IsSmall%>",
		                    sw:"<%=SwfUploadInfo.SmallWidth%>",
		                    sh:"<%=SwfUploadInfo.SmallHeight%>",
		                    wm:"<%=SwfUploadInfo.IsWaterMark%>",
		                    data:""
                        },
            file_size_limit: "<%=SwfUploadInfo.File_size_limit%> MB",
		    file_types: "<%=SwfUploadInfo.File_types%>",
		    file_types_description: "<%=SwfUploadInfo.File_types_description%>",
		    file_upload_limit: <%=SwfUploadInfo.UploadMode.ToString()=="BUTTON"?"1":(SwfUploadInfo.File_upload_limit - SwfUploadInfo.Exist_file_count).ToString()%>,
		    button_action:SWFUpload.BUTTON_ACTION.<%=SwfUploadInfo.Button_action%>,
		    button_disabled : <%=((SwfUploadInfo.File_upload_limit - SwfUploadInfo.Exist_file_count)<= 0 && SwfUploadInfo.File_upload_limit!=0)  ? "true" : "false" %>,
		    button_placeholder_id:  "<%= this.GetControlId("spanButtonPlaceholder") %>",
		    upload_success_handler:<%= this.GetControlId("uploadSuccess") %>,
            custom_settings: {
                upload_target: "<%= this.GetControlId("divFileProgressContainer") %>",
                submitBtnId: "<%=SwfUploadInfo.SubmitButtonId%>",
                serverDataId: "<%=this.hidIdList.ClientID%>",
                uploadMode: "<%=SwfUploadInfo.UploadMode%>"
            }
        }
        SWFLoad(<%= this.GetControlId("id") %>,LoadSettings);
    }
    addLoadEvent(<%= this.GetControlId("load") %>);
    function <%= this.GetControlId("uploadSuccess") %>(file, serverData) {
        uploadSuccess(file, serverData,this);
    }
</script>
<asp:HiddenField runat="server" ID="hidIdList" Value="" />
<asp:HiddenField runat="server" ID="hidON" Value="" />
<asp:Literal runat="server" ID="lalHtml"></asp:Literal>
