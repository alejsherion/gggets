﻿/*初始化开始*/
//var swfu;
function SWFLoad(swfu, loadSettings) {
    var bb;
    swfu.SWFObj = new SWFUpload({
        upload_url: "../swfupload/upload.aspx",
        post_params: loadSettings.post_params,

        file_size_limit: loadSettings.file_size_limit,
        file_types: loadSettings.file_types,
        file_types_description: loadSettings.file_types_description,
        file_upload_limit: loadSettings.file_upload_limit,

        file_queue_error_handler: fileQueueError,
        file_dialog_complete_handler: fileDialogComplete,
        upload_progress_handler: uploadProgress,
        upload_error_handler: uploadError,
        upload_success_handler: loadSettings.upload_success_handler,
        upload_complete_handler: uploadComplete,

        button_action: loadSettings.button_action,
        button_cursor: SWFUpload.CURSOR.HAND,
        button_disabled: loadSettings.button_disabled,
        button_image_url: "../swfupload/images/swfupload_uploadBtn.png",
        button_placeholder_id: loadSettings.button_placeholder_id,
        button_width: 62,
        button_height: 22,
        button_text_top_padding: 1,
        button_text_left_padding: 5,

        flash_url: "../swfupload/swf/swfupload.swf",

        custom_settings: loadSettings.custom_settings,
        debug: false
    });
}
//替代window.onload   解决不能同时使用多个window.onload的错误
function addLoadEvent(func) {
    var oldonload = window.onload;
    if (typeof window.onload != "function") {
        window.onload = func;
    } else {
        window.onload = function() {
            oldonload();
            func();
        }
    }
}

/*初始化结束*/
/*为异步请求数据提供支持   开始*/
var isOk;
var xmlHttpRequest = null;
function DelPic(fileInfo, swfObj) {


    //var name = id + extension;
    var url = "../swfupload/upload.aspx?cmd=del&f=" + fileInfo.Path + "&name=" + fileInfo.FileName + "&dt=" + new Date();
    //删除指定ServerData值
    var serverData = document.getElementById(swfObj.customSettings.serverDataId).value;
    var sData = JSON.parse(serverData);
    for (var i = 0; i < sData.length; i++) {
        if (sData[i].Id == fileInfo.Id) {
            sData.splice(i, 1);
            break;
        }
    }
    var newSData = JSON.stringify(sData);
    document.getElementById(swfObj.customSettings.serverDataId).value = newSData;

    xmlHttpRequest = getXMLHttpRequest();
    try {
        xmlHttpRequest.open("get", url, false);
        xmlHttpRequest.onreadystatechange = onReadyStateChange;
        xmlHttpRequest.send(null);
    }
    catch (e) {
        alert(e);
    }
    return isOk;
}
function onReadyStateChange() {
    try {
        if (xmlHttpRequest.readyState == 4) {
            if (xmlHttpRequest.status == 200) {
                if (xmlHttpRequest.responseText != "删除成功!") {
                    alert(xmlHttpRequest.responseText);
                }
            }
        }
    }
    catch (e) {
        alert(e);
    }
}
function getXMLHttpRequest() {
    if (window.XMLHttpRequest) {
        return new XMLHttpRequest();
    }
    else if (window.ActiveXObject) {
        var request = new ActiveXObject("Microsoft.XMLHTTP");
        if (!request) {
            request = new ActiveXObject("Msxml2.XHLHTTP");
        }
        return request;
    }
}
/*为异步请求数据提供支持   结束*/

function fileQueueError(file, errorCode, message) {
    try {
        var errorMsg = "";
        //debugger;
        switch (errorCode) {
            case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
                if (message > 0) {
                    errorMsg = "您只能上传" + message + "个文件!";
                } else {
                    errorMsg = "您不可继续上传文件!";
                }
                break;
            case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                errorMsg = "您不可上传0字节的文件";
                break;
            case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                errorMsg = "文件不可大于" + this.settings.file_size_limit;
                break;
            case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
            case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
            default:
                alert(message);
                break;
        }
        if (errorMsg !== "") {
            alert(errorMsg);
            return;
        }
        //addImage("images/" + imageName);
    } catch (ex) {
        this.debug(ex);
    }

}

function fileDialogComplete(numFilesSelected, numFilesQueued) {
    try {
        if (numFilesQueued > 0) {
            this.startUpload();
            //禁用提交按钮
            document.getElementById(this.customSettings.submitBtnId).disabled = true;
        }
    } catch (ex) {
        this.debug(ex);
    }
}

function uploadProgress(file, bytesLoaded) {

    try {
        var percent = Math.ceil((bytesLoaded / file.size) * 100);

        var progress = GetFileProgressObject(this.customSettings, file, this.customSettings.upload_target);
        progress.setProgress(percent);
        if (percent === 100) {
            //progress.setStatus("正在生成缩略图...");
            progress.toggleCancel(false, this);
        } else {
            //progress.setStatus("正在上传...");
            progress.toggleCancel(true, this);
        }
    } catch (ex) {
        this.debug(ex);
    }
}

//定义常用属性
var CustomSettingsInfo =
{
    //可预览的图片类型
    imgType: new Array(".jpg", ".gif", ".png", ".bmp", ".jpeg"),
    //判断是否为可预览的图片类型
    isImg: function(currImgType) {
        currImgType = currImgType.toLowerCase();
        for (var i = 0; i < this.imgType.length; i++) {
            if (this.imgType[i] == currImgType) {
                return true;
            }
        }
        return false;
    }
};

function uploadSuccess(file, serverData, swfObj) {
    try {

        var progress = GetFileProgressObject(swfObj.customSettings, file, swfObj.customSettings.upload_target);
        //更新serverData
        document.getElementById(swfObj.customSettings.serverDataId).value = serverData;
        swfObj.setPostParams({
            ASPSESSID: swfObj.settings.post_params.ASPSESSID,
            path: swfObj.settings.post_params.path,
            fn: swfObj.settings.post_params.fn,
            small: swfObj.settings.post_params.small,
            sw: swfObj.settings.post_params.sw,
            sh: swfObj.settings.post_params.sh,
            wm: swfObj.settings.post_params.wm,
            data: serverData
        });
        var dataList = JSON.parse(serverData);
        var fileInfo = "";
        if (dataList.length > 0) {
            fileInfo = dataList[dataList.length - 1];
        }

        progress.fileProgressWrapper.childNodes[0].childNodes[0].href = "javascript:";
        //如果为图片类型   则可使用图片预览功能
        if (CustomSettingsInfo.isImg(file.type) && swfObj.customSettings.uploadMode != "BUTTON") {
            //图片路径
            progress.fileProgressWrapper.childNodes[0].childNodes[1].childNodes[0].src = fileInfo.Path + "b/" + fileInfo.FileName;
            
            //计算图片尺寸
            var width = fileInfo.Width;
            var height = fileInfo.Height;
            var newWidth = 200;
            var newHeight = 0;
            if (width < 200) { newWidth = width; }
            newHeight = newWidth * height / width;

            progress.fileProgressWrapper.childNodes[0].childNodes[1].childNodes[0].width = newWidth;
            progress.fileProgressWrapper.childNodes[0].childNodes[1].childNodes[0].height = newHeight;
            
            //显示or隐藏图片
            progress.fileProgressWrapper.childNodes[0].childNodes[0].onmouseout = function() {
                progress.fileProgressWrapper.childNodes[0].childNodes[1].style.display = 'none';
            }
            progress.fileProgressWrapper.childNodes[0].childNodes[0].onmouseover = function() {
                progress.fileProgressWrapper.childNodes[0].childNodes[1].style.display = '';
            }
        }
        progress.setStatus("上传成功");
        progress.toggleCancel(false);


        if (swfObj.customSettings.uploadMode != "BUTTON") {
            progress.fileProgressWrapper.childNodes[3].childNodes[0].onclick = function() {
                if (confirm("删除后无法恢复,是否继续?")) {
                    DelPic(fileInfo, swfObj);
                    removeElement(progress.fileProgressWrapper);
                }
            }
            progress.fileProgressWrapper.childNodes[3].childNodes[0].style.visibility = "visible";
        }
    } catch (ex) {
        //this.debug(ex);
    }
}

//删除指定节点下所有节点
function removeElement(_element) {
    var _parentElement = _element.parentNode;
    if (_parentElement) {
        _parentElement.removeChild(_element);
    }
}

//全部文件上传完毕
function uploadComplete(file) {
    try {
        if (this.getStats().files_queued > 0) {
            this.startUpload();
        } else {
            //启用提交按钮
            document.getElementById(this.customSettings.submitBtnId).disabled = false;
        }
    } catch (ex) {
        this.debug(ex);
    }
}

function uploadError(file, errorCode, message) {
    var imageName = "error.gif";
    var progress;
    try {
        switch (errorCode) {
            case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
                try {
                    progress = GetFileProgressObject(this.customSettings, file, this.customSettings.upload_target);
                    progress.setCancelled();
                    progress.setStatus("已取消");
                    progress.toggleCancel(false);
                }
                catch (ex1) {
                    this.debug(ex1);
                }
                break;
            case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
                try {
                    progress = GetFileProgressObject(this.customSettings, file, this.customSettings.upload_target);
                    progress.setCancelled();
                    progress.setStatus("已停止");
                    progress.toggleCancel(true);
                }
                catch (ex2) {
                    this.debug(ex2);
                }
            default:
                alert(message);
                break;
        }
    } catch (ex3) {
        this.debug(ex3);
    }

}
//获取FileProgress对象
function GetFileProgressObject(cSettings, file, targetID) {
    if (cSettings.uploadMode == "BUTTON")
        return new FileProgress_ButtomMode(file, targetID);
    else
        return new FileProgress(file, targetID);
}


/* ******************************************
*	控制对象显示HTML信息
* ****************************************** */
function FileProgress(file, targetID) {
    this.fileProgressID = file.id;
    this.fileProgressWrapper = document.getElementById(this.fileProgressID);
    if (!this.fileProgressWrapper) {

        this.fileProgressWrapper = document.createElement("li");
        this.fileProgressWrapper.className = "swfupload_li";
        this.fileProgressWrapper.id = this.fileProgressID;

        //文件名
        var fileNameDiv = document.createElement("div");
        fileNameDiv.className = "swfupload_pic_name";
        var fileNameA = document.createElement("a");
        fileNameA.appendChild(document.createTextNode(file.name));

        //缩略图片
        var imgSpan = document.createElement("span");
        imgSpan.style.display = 'none';
        var imgFile = document.createElement("img");
        imgFile.src = "#";
        //        imgFile.width = 80;
        //        imgFile.height = 80;
        imgSpan.appendChild(imgFile);
        fileNameDiv.appendChild(fileNameA);
        fileNameDiv.appendChild(imgSpan);

        //上传状态
        var fileSizeDiv = document.createElement("div");
        fileSizeDiv.className = "swfupload_pic_state";
        var fileStateLabel = document.createElement("label");
        fileSizeDiv.appendChild(fileStateLabel);

        //上传状态
        var fileUploadStateDiv = document.createElement("div");
        fileUploadStateDiv.className = "swfupload_pic_option";
        var progressBar = document.createElement("div");
        progressBar.className = "swfUpload_progressBarInProgress";
        fileUploadStateDiv.appendChild(progressBar);

        //文件操作
        var deleteDiv = document.createElement("div");
        deleteDiv.className = "swfupload_pic_percent";
        deleteDiv.id = "swfupload_pic_percent";
        var deleteA = document.createElement("a");
        deleteA.className = "swfupload_pic_del";
        deleteA.href = "#";
        deleteA.style.visibility = "hidden";
        deleteA.appendChild(document.createTextNode(" "));
        deleteDiv.appendChild(deleteA);

        //添加节点
        this.fileProgressWrapper.appendChild(fileNameDiv);
        this.fileProgressWrapper.appendChild(fileSizeDiv);
        this.fileProgressWrapper.appendChild(fileUploadStateDiv);
        this.fileProgressWrapper.appendChild(deleteDiv);

        document.getElementById(targetID).appendChild(this.fileProgressWrapper);
    } else {
    }
    this.height = this.fileProgressWrapper.offsetHeight;
}


FileProgress.prototype.setProgress = function(percentage) {
    this.fileProgressWrapper.className = "swfUpload_progressContainer swfUpload_blue";
    this.fileProgressWrapper.childNodes[2].childNodes[0].className = "swfUpload_progressBarInProgress";
    this.fileProgressWrapper.childNodes[2].childNodes[0].style.width = percentage + "%";
    this.fileProgressWrapper.childNodes[1].childNodes[0].innerHTML = "已经上传:" + percentage + "%";
};

FileProgress.prototype.setComplete = function() {
    this.fileProgressWrapper.className = "swfUpload_progressContainer green";
    this.fileProgressWrapper.childNodes[2].childNodes[0].className = "swfUpload_progressBarComplete";
    this.fileProgressWrapper.childNodes[2].childNodes[0].style.width = "";
};

FileProgress.prototype.setError = function() {
    this.fileProgressWrapper.className = "swfUpload_progressContainer swfUpload_red";
    this.fileProgressWrapper.childNodes[2].childNodes[0].className = "swfUpload_progressBarError";
    this.fileProgressWrapper.childNodes[2].childNodes[0].style.width = "";
};

FileProgress.prototype.setCancelled = function() {
    this.fileProgressWrapper.className = "swfUpload_progressContainer";
    this.fileProgressWrapper.childNodes[2].childNodes[0].className = "swfUpload_progressBarError";
    this.fileProgressWrapper.childNodes[2].childNodes[0].style.width = "";
};

FileProgress.prototype.setStatus = function(status) {
    this.fileProgressWrapper.childNodes[1].childNodes[0].innerHTML = status;
};

FileProgress.prototype.toggleCancel = function(show, swfuploadInstance) {
    this.fileProgressWrapper.childNodes[3].childNodes[0].style.visibility = show ? "visible" : "hidden";
    if (swfuploadInstance) {
        var fileID = this.fileProgressID;
        this.fileProgressWrapper.childNodes[3].childNodes[0].onclick = function() {
            swfuploadInstance.cancelUpload(fileID);
            return false;
        };
    }
};


function FileProgress_ButtomMode(file, targetID) {
    this.fileProgressID = file.id;
    this.fileProgressWrapper = document.getElementById(this.fileProgressID);
    if (!this.fileProgressWrapper) {
        //上传状态
        this.fileProgressWrapper = document.createElement("div");
        this.fileProgressWrapper.className = "divFileProgressContainer_BottomMode";
        this.fileProgressWrapper.id = this.fileProgressID;

        var divUpState = document.createElement("div");
        divUpState.className = "swfUpload_progressBarInProgress_BottomMode";
        var upState = document.createElement("label");
        divUpState.appendChild(upState);
        this.fileProgressWrapper.appendChild(divUpState);

        document.getElementById(targetID).appendChild(this.fileProgressWrapper);

    } else {
    }
}

FileProgress_ButtomMode.prototype.setProgress = function(percentage) {
    this.fileProgressWrapper.childNodes[0].style.width = percentage + "%";
    this.fileProgressWrapper.childNodes[0].childNodes[0].innerHTML = percentage + "%";
};
FileProgress_ButtomMode.prototype.setComplete = function() { };
FileProgress_ButtomMode.prototype.setError = function() { };
FileProgress_ButtomMode.prototype.setCancelled = function() { };
FileProgress_ButtomMode.prototype.setStatus = function(status) { };
FileProgress_ButtomMode.prototype.toggleCancel = function(show, swfuploadInstance) { };