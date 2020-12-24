var configurator = {

addCustomProductOnCart: function(details, img1, img2, img3) {
	console.log("INPUT STRINT", Pointer_stringify(details));
	
	var detailsobj = JSON.parse(Pointer_stringify(details));
		
	var param = "";
	if(Pointer_stringify(img1) !== "" && Pointer_stringify(img2) === "" && Pointer_stringify(img3) === "" )
	{
		detailsobj.images = {left: Pointer_stringify(img1)};	
	}
	else if(Pointer_stringify(img1) === "" && Pointer_stringify(img2) !== "" && Pointer_stringify(img3) === "" )
	{
		detailsobj.images = {middle: Pointer_stringify(img2)};
	}
	else if(Pointer_stringify(img1) === "" && Pointer_stringify(img2) === "" && Pointer_stringify(img3) !== "" )
	{
		detailsobj.images = {right: Pointer_stringify(img3)};
	}
	else if(Pointer_stringify(img1) !== "" && Pointer_stringify(img2) !== "" && Pointer_stringify(img3) === "" )
	{
		detailsobj.images = {left: Pointer_stringify(img1), middle: Pointer_stringify(img2)};
	}
	else if(Pointer_stringify(img1) !== "" && Pointer_stringify(img2) === "" && Pointer_stringify(img3) !== "" )	
	{
		detailsobj.images = {left: Pointer_stringify(img1), right: Pointer_stringify(img3)};
	}
	else if(Pointer_stringify(img1) === "" && Pointer_stringify(img2) !== "" && Pointer_stringify(img3) !== "" )
	{
		detailsobj.images = {middle: Pointer_stringify(img2), right: Pointer_stringify(img3)};
	}
	else if(Pointer_stringify(img1) !== "" && Pointer_stringify(img2) !== "" && Pointer_stringify(img3) !== "" )
	{
		detailsobj.images = {left: Pointer_stringify(img1), middle: Pointer_stringify(img2), right: Pointer_stringify(img3)};
	}
		
    console.log("ADD CUSTOM PRODUCT", detailsobj);
    parent.postMessage({from: "configurator", product: detailsobj}, "*");
	
},

addPredefineProductToChart:function(details)
{
	var detailsobj = JSON.parse(Pointer_stringify(details));	
    parent.postMessage({from: "configuratorPredefine", product: detailsobj}, "*");
},

addCustomProductOnCartSample: function(details) {
    console.log("ADD CUSTOM PRODUCT", details);
    $.ajax({
        url: "https://siligin.com/safe-jawz/logo.png",
        type: "GET",
        xhrFields: {
            withCredentials: true
        },
        mimeType: "text/plain; charset=x-user-defined"
    }).done(function( data, textStatus, jqXHR ) {
        details.images = {left: data};
        parent.postMessage({from: "configurator", product: details}, "*");
    }).fail(function( jqXHR, textStatus, errorThrown ) {
        alert("fail: " + errorThrown);
    });
},

onPriceChange: function(value) {
    console.log("NEW PRICE", value);
    parent.postMessage({from: "configurator", price: value}, "*");
},

openMyUrl: function (myurl, inNewTab)
{
	//console.log(Pointer_stringify(myurl));
	if(inNewTab)
		window.open(Pointer_stringify(myurl), '_blank');
	else
		window.open(Pointer_stringify(myurl), '_parent');
}

};

mergeInto(LibraryManager.library, configurator);