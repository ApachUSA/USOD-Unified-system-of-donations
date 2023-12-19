function ChangeImage(ImageURL) {
	var previewImage = document.getElementById("Donor_Avatar");

		previewImage.src = ImageURL;

}

function isValidURL(url) {
	var img = new Image();
	img.src = url;
	return img.complete;
}