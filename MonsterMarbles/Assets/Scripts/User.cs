
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
		string FbID {
			get {
				return this.fbID;
			}
			set {
				fbID = value;
			}
		}

		string FirstName {
			get {
				return this.firstName;
			}
			set {
				firstName = value;
			}
		}

		string LastName {
			get {
				return this.lastName;
			}
			set {
				lastName = value;
			}
		}

		string Language {
			get {
				return this.language;
			}
			set {
				language = value;
			}
		}

		string City {
			get {
				return this.city;
			}
			set {
				city = value;
			}
		}

		string Picture {
			get {
				return this.picture;
			}
			set {
				picture = value;
			}
		}

		string Creation_timestamp {
			get {
				return this.creation_timestamp;
			}
			set {
				creation_timestamp = value;
			}
		}

		IList Unlockables {
			get {
				return this.unlockables;
			}
			set {
				unlockables = value;
			}
		}

		int Skybits {
			get {
				return this.skybits;
			}
			set {
				skybits = value;
			}
		}

		int ZoogiBucks {
			get {
				return this.zoogiBucks;
			}
			set {
				zoogiBucks = value;
			}
		}

	}

}

