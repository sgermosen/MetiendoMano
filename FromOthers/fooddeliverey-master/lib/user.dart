class User {
  String id;
  String firstName;
  String lastName;


  User(
    this.id,
    this.firstName,
    this.lastName,
  );
  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      json['id'] as String,
      json['first_name'] as String,

      json['last_name'] as String,
      // json['email'] as String,
      // json['address'] as String,
      // json['serviceCharge'] as String,
    );
  }
}
