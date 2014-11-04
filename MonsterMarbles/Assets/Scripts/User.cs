
using System;
using System.Collections;
namespace AssemblyCSharp
{
	public class User
	{
		private string fbID;
		private string firstName;
		private string lastName;
		private string language;
		private string city;
		private string picture;
		private string creation_timestamp;
		private IList unlockables= new ArrayList();
		private int skybits;
		private int zoogiBucks;

		public User (string fbID, string firstName, string lastName, string language, string city, string picture, string creation_timestamp, IList unlockables, int skybits, int zoogiBucks)
		{
			this.fbID = fbID;
			this.firstName = firstName;
			this.lastName = lastName;
			this.language = language;
			this.city = city;
			this.picture = picture;
			this.creation_timestamp = creation_timestamp;
			this.unlockables = unlockables;
			this.skybits = skybits;
			this.zoogiBucks = zoogiBucks;
		}

		public string getFbID() {
			return fbID;
		}
		public void setFbID(string fbID) {
			this.fbID = fbID;
		}
		public string getFirstName() {
			return firstName;
		}
		public void setFirstName(string firstName) {
			this.firstName = firstName;
		}
		public string getLastName() {
			return lastName;
		}
		public void setLastName(string lastName) {
			this.lastName = lastName;
		}
		public string getLanguage() {
			return language;
		}
		public void setLanguage(string language) {
			this.language = language;
		}
		public string getCity() {
			return city;
		}
		public void setCity(string city) {
			this.city = city;
		}
		public string getPicture() {
			return picture;
		}
		public void setPicture(string picture) {
			this.picture = picture;
		}
		public string getCreation_timestamp() {
			return creation_timestamp;
		}
		public void setCreation_timestamp(string creation_timestamp) {
			this.creation_timestamp = creation_timestamp;
		}
		public IList getUnlockables() {
			return unlockables;
		}
		public void setUnlockables(IList unlockables) {
			this.unlockables = unlockables;
		}
		public int getSkybits() {
			return skybits;
		}
		public void setSkybits(int skybits) {
			this.skybits = skybits;
		}
		public int getZoogiBucks() {
			return zoogiBucks;
		}
		public void setZoogiBucks(int zoogiBucks) {
			this.zoogiBucks = zoogiBucks;
		}

	}

}

