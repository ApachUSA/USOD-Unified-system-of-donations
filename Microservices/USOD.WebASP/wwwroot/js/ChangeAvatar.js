function ChangeImage(ImageURL) {
	var previewImage = document.getElementById("Donor_Avatar");

	if (isValidURL(ImageURL)) {
		previewImage.src = ImageURL;
	} else {
		previewImage.src = 'https://cdn-icons-png.flaticon.com/512/6614/6614285.png';
	}
}

function isValidURL(url) {
	var img = new Image();
	img.src = url;
	return img.complete;
}