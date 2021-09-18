import 'package:flutter/material.dart';
import 'package:fooddelivery/service.dart';
import 'package:fooddelivery/user.dart';

void main() => runApp(MyAPp());

class MyAPp extends StatefulWidget {
  @override
  _MyAPpState createState() => _MyAPpState();
}

class _MyAPpState extends State<MyAPp> {
  List<User> users;
  User user;
  TextEditingController firstName = TextEditingController();
  TextEditingController lastName = TextEditingController();

  @override
  void initState() {
    super.initState();
    users = [];
    getuser();
  }

  getuser() {
    Services.getUserData().then((value) {
      setState(() {
        users = value;
      });
      print(users.length);
    });
  }

  clearvalue() {
    firstName.text = '';
    lastName.text = '';
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(
          title: Text('TestDb'),
          actions: <Widget>[
            IconButton(
                icon: Icon(Icons.add),
                onPressed: () {
                  _createdb();
                })
          ],
        ),
        body: Column(
          children: <Widget>[
            //Text(user.id),
            TextFormField(
              controller: firstName,
            ),
            TextFormField(
              controller: lastName,
            ),
            // OutlineButton(
            //   onPressed: () {
            //     adduser().then(getuser());
            //   },
            // ),
            OutlineButton(
              child: Text('update'),
              onPressed: () {
                update(user);
              },
            ),
            Flexible(
              child: Container(
                height: 400,
                color: Colors.green,
                child: ListView(
                  children: <Widget>[
                    for (int i = 0; i < users.length; i++)
                      Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: <Widget>[
                          Text(users[i].id),
                          Text(users[i].firstName),
                          Text(users[i].lastName),
                          IconButton(
                              icon: Icon(Icons.delete),
                              onPressed: () {
                                delete(users[i].id);
                                setState(() {
                                  users.removeAt(i);
                                });
                              }),
                          IconButton(
                              icon: Icon(Icons.edit),
                              onPressed: () {
                                setState(() {
                                  user = users[i];
                                });
                                showvalues(user);
                              })
                        ],
                      )
                  ],
                ),
              ),
            )
          ],
        ),
      ),
    );
  }

  update(user) {
    Services.updateUser(user.id, firstName.text, lastName.text).then(getuser());
  }

  adduser() {
    Services.addUser(firstName.text, lastName.text).then(clearvalue());
  }

  delete(id) {
    Services.deleteEmployee(id).then(print);
    // getuser();
  }

  _createdb() {
    Services.createTable().then((value) {
      print(value);
    });
  }

  void showvalues(user) {
    firstName.text = user.firstName;
    lastName.text = user.lastName;
  }
}
