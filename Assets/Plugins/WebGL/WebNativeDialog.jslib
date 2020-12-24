var WebNativeDialog = {
  NativeDialogPrompt:function (title , defaultValue){
    defaultValue = Pointer_stringify(defaultValue);
    title = Pointer_stringify(title);
    var result = window.prompt( title , defaultValue );
    if( !result ){
      result = defaultValue;
    }
    var buffer = _malloc(lengthBytesUTF8(result) + 1);
    writeStringToMemory(result, buffer);
    return buffer;
  },
  SetupOverlayDialogHtml:function(title,defaultValue,okBtnText,cancelBtnText){
    title = Pointer_stringify(title);
    defaultValue = Pointer_stringify(defaultValue);
    okBtnText = Pointer_stringify(okBtnText);
    cancelBtnText = Pointer_stringify(cancelBtnText);

    if( !document.getElementById("nativeInputDialogInput" ) ){
      // setup css
      var style = document.createElement( 'style' );
      style.setAttribute('id' , 'inputDialogTextSelect');
      style.appendChild( document.createTextNode( '#nativeInputDialogInput::-moz-selection { background-color:#00ffff;}' ) );
      style.appendChild( document.createTextNode( '#nativeInputDialogInput::selection { background-color:#00ffff;}' ) );
      document.head.appendChild( style );
    }
    if( !document.getElementById("nativeInputDialog" ) ){
      // setup html
      var html ='<div id="nativeInputDialog" style="background: rgba(0,0,0,0.8);width:100%;height:100%;position:fixed;top:0%;z-index:2147483647;">' +
                '<div style="position:relative;top:30%;" align="center" vertical-align="middle">' +
                   '<div id="nativeInputDialogTitle" class="myHeaderText">Input text </div>' +
                   '<div style="margin-top:20px">' +
                     '<input id="nativeInputDialogInput" class="myInputField"  type="text" size="40" onsubmit="">' +
                   '</div><div style="margin-top:20px">' +
                     '<input id="nativeInputDialogOkBtn" class="myButton" type="button" value="OK" onclick="" >' +
                     '<input id="nativeInputDialogCancelBtn" class="myButton" type="button" value="Cancel" onclick ="">' +
                     '<input id="nativeInputDialogCheck" class="myButton" type="checkBox" style="display:none;">' +
                   '</div></div></div>' +
			    '<style>.myHeaderText {color:#FFFFFF; font-family:Arial;font-size:25px;}.myInputField {	-moz-box-shadow:inset 0px 1px 0px 0px #f5978e;	-webkit-box-shadow:inset 0px 1px 0px 0px #f5978e;	box-shadow:inset 0px 1px 0px 0px #f5978e; height: 5%; width: 300px;	background-color:#FFFFFF;	-moz-border-radius:6px;	-webkit-border-radius:6px;	border-radius:6px;	border:1px solid #d02718;	display:inline-block;	cursor:pointer;	color:#1E1E1E;font-family:Arial;font-size:15px;	padding:6px 24px;text-decoration:none;text-shadow:0px 1px 0px #810e05;}				.myButton {	-moz-box-shadow:inset 0px 1px 0px 0px #9D1B25;				-webkit-box-shadow:inset 0px 1px 0px 0px #9D1B25;					box-shadow:inset 0px 1px 0px 0px #9D1B25;					background-color:#9D1B25;					-moz-border-radius:6px;					-webkit-border-radius:6px;					opacity:1;		margin: 1px 10px 75px 1px;		border-radius:6px;					border:1px solid #9D1B25;					display:inline-block;					cursor:pointer;		-webkit-appearance: none;			color:#ffffff;					font-family:Arial;					font-size:15px;					font-weight:bold;					padding:6px 24px;					text-decoration:none;					text-shadow:0px 1px 0px #810e05;}				.myButton:hover {	background-color:#9D1B25;}				.myButton:active {	position:relative;	top:1px;}</style>';
      var element = document.createElement('div');
      element.innerHTML = html;
      // write to html
      document.body.appendChild( element );

      // set Event
      var okFunction = 
        'document.getElementById("nativeInputDialog" ).style.display = "none";' + 
        'document.getElementById("nativeInputDialogCheck").checked = false;' 
		//+'document.getElementById("canvas").style.display="";'
		;
      var cancelFunction = 
        'document.getElementById("nativeInputDialog" ).style.display = "none";'+ 
        'document.getElementById("nativeInputDialogCheck").checked = true;'
		//+'document.getElementById("canvas").style.display="";'
		;


      var inputField = document.getElementById("nativeInputDialogInput");
      inputField.setAttribute( "onsubmit" , okFunction );
      var okBtn = document.getElementById("nativeInputDialogOkBtn");
      okBtn.setAttribute( "onclick" , okFunction );
      var cancelBtn = document.getElementById("nativeInputDialogCancelBtn");
      cancelBtn.setAttribute( "onclick" , cancelFunction );
    }
    document.getElementById("nativeInputDialogTitle").innerText = title;
    document.getElementById("nativeInputDialogInput").value= defaultValue;

    document.getElementById("nativeInputDialogOkBtn").value = okBtnText;
    document.getElementById("nativeInputDialogCancelBtn").value = cancelBtnText;
    document.getElementById("nativeInputDialog" ).style.display = "";
  },
  HideUnityScreenIfHtmlOverlayCant:function(){
    if( navigator.userAgent.indexOf("Chrome/") < 0 )
	{
    //  document.getElementById("canvas").style.display="none";
    }
  },
  IsRunningOnEdgeBrowser:function(){
    if( navigator.userAgent.indexOf("Edge/") < 0 ){
      return false;
    }
    return true;
  },
  IsOverlayDialogHtmlActive:function(){
    var nativeDialog = document.getElementById("nativeInputDialog" );
    if( !nativeDialog ){
      return false;
    }
    return ( nativeDialog.style.display != 'none' );
  },
  IsOverlayDialogHtmlCanceled:function(){
    var check = document.getElementById("nativeInputDialogCheck");
    if( !check ){ return false; }
    return check.checked;
  },
  GetOverlayHtmlInputFieldValue:function(){
    var inputField = document.getElementById("nativeInputDialogInput");
    var result = "";
    if( inputField && inputField.value ){
      result = inputField.value;
    }
	console.log(result);
	//return result;
	var bufferSize = lengthBytesUTF8(result) + 1;
    var buffer = _malloc(bufferSize);
	console.log(result);
    //writeStringToMemory(result, buffer);
	 stringToUTF8(result, buffer, bufferSize);
	 console.log(result);
	console.log(buffer);
    return buffer;
  }

};
mergeInto( LibraryManager.library , WebNativeDialog );

