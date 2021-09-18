import 'dart:convert';
import 'package:http/http.dart' as http;

import 'user.dart';

class Services {
  static const ROOT = 'http://192.168.56.1:8080/foodapp/foodapp.php';
  static const _CREATE_TABLE_ACTION = 'CREATE_TABLE';
  static const _GET_ALL_ACTION = 'GET_ALL';
  static const _ADD_User_ACTION = 'ADD_EMP';
  static const _UPDATE_EMP_ACTION = 'UPDATE_EMP';
  static const _DELETE_EMP_ACTION = 'DELETE_EMP';

  // Method to create the table Employees.
  static Future<String> createTable() async {
    try {
      // add the parameters to pass to the request.
      var map = Map<String, dynamic>();
      map['action'] = _CREATE_TABLE_ACTION;
      final response = await http.post(ROOT, body: map);
      print('Create Table Response: ${response.body}');
      if (200 == response.statusCode) {
        return response.body;
      }
      return 'error';
    } catch (e) {
      return "error";
    }
  }

  static Future<List<User>> getUserData() async {
    try {
      var map = Map<String, dynamic>();
      map['action'] = _GET_ALL_ACTION;
      final response = await http.post(ROOT, body: map);
      print('getUsers Response: ${response.body}');
      if (200 == response.statusCode) {
        //print(response.statusCode);
        List<User> list = parseResponse(response.body);
        return list;
      } else {
        return List<User>();
      }
    } catch (e) {
      return List<User>(); // return an empty list on exception/error
    }
  }

  static List<User> parseResponse(String responseBody) {
    final parsed = json.decode(responseBody).cast<Map<String, dynamic>>();
    return parsed.map<User>((json) => User.fromJson(json)).toList();
  }

  // Method to add employee to the database...
  static Future<String> addUser(String firstName, String lastName) async {
    try {
      var map = Map<String, dynamic>();
      map['action'] = _ADD_User_ACTION;
      map['first_name'] = firstName;
      map['last_name'] = lastName;
      // map['email'] = email;
      final response = await http.post(ROOT, body: map);

      ///print('addEmployee Response: ${response.body}');
      if (200 == response.statusCode) {
        return response.body;
      } else {
        return "error";
      }
    } catch (e) {
      return "error";
    }
  }

  // Method to update an Employee in Database...
  static Future<String> updateUser(
      String id, String firstName, String lastName) async {
    try {
      var map = Map<String, dynamic>();
      map['action'] = _UPDATE_EMP_ACTION;
      map['id'] = id;
      map['first_name'] = firstName;
      map['last_name'] = lastName;
      // map['email'] = email;
      final response = await http.post(ROOT, body: map);
      //print('updateEmployee Response: ${response.body}');
      if (200 == response.statusCode) {
        return response.body;
      } else {
        return "error";
      }
    } catch (e) {
      return "error";
    }
  }

  // Method to Delete an Employee from Database...
  static Future<String> deleteEmployee(String id) async {
    try {
      var map = Map<String, dynamic>();
      map['action'] = _DELETE_EMP_ACTION;
      map['id'] = id;
      final response = await http.post(ROOT, body: map);
      // print('deleteEmployee Response: ${response.body}');
      if (200 == response.statusCode) {
        return response.body;
      } else {
        return "error";
      }
    } catch (e) {
      return "error"; // returning just an "error" string to keep this simple...
    }
  }
}
