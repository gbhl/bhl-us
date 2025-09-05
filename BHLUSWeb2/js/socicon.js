var socialSpan = document.getElementById("socicons");
if (socialSpan) {
	addSocialElement(socialSpan, "https://bsky.app/profile/biodivlibrary.bsky.social", "socicon socicon-bsky", "/images/socicon-bsky.png", "Open BlueSky", "BlueSky");
	addSocialElement(socialSpan, "https://instagram.com/biodivlibrary", "socicon socicon-insta", "/images/socicon-insta.png", "Open Instagram", "Instagram");
	addSocialElement(socialSpan, "https://facebook.com/biodivlibrary", "socicon socicon-fb", "/images/socicon-fb.png", "Open Facebook", "Facebook");
	addSocialElement(socialSpan, "https://flickr.com/biodivlibrary", "socicon socicon-flickr", "/images/socicon-flickr.png", "Open Flickr", "Flickr");
}

function addSocialElement(target, url, cssclass, icon, alt, title) {
	const newA = document.createElement("a");
	newA.setAttribute('href', url);
	newA.setAttribute('target', '_blank')	;
	const newSpan = document.createElement("span");
	newSpan.setAttribute('class', cssclass);
	const newImg = document.createElement("img");
	newImg.setAttribute('src', icon);
	newImg.setAttribute('alt', alt);
	newImg.setAttribute('title', title);
	newSpan.appendChild(newImg)
	newA.appendChild(newSpan);
	target.appendChild(newA);
}