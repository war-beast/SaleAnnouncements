import ListingPage from "Pages/listing/listingPage"

try {
	globalWindowObject.listingPage = new ListingPage();
} catch (ex) {
	console.error(ex);
}