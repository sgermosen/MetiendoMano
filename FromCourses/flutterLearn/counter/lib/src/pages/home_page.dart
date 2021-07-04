import 'package:flutter/material.dart';

class HomePage extends StatelessWidget {
  final _estiloText = new TextStyle(fontSize: 30);
  final _conteo = 10;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Titulo'),
      ),
      body: Center(
          child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Text(
            'Hola Mundo',
            style: _estiloText,
          ),
          Text(
            '$_conteo',
            style: _estiloText,
          ),
        ],
      )),
      floatingActionButtonLocation: FloatingActionButtonLocation.centerFloat,
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          print('Hola mundo');
        },
        child: Icon(Icons.add),
      ),
    );
  }
}
