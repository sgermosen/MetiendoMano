package com.vacuum.app.metquiz.Utils.Expandable;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class ExpandableListDataPump {
    public static HashMap<String, List<String>> getData() {
        HashMap<String, List<String>> expandableListDetail = new HashMap<String, List<String>>();

        List<String> cricket = new ArrayList<String>();
        cricket.add("Is PHP A Programing Language ?");
        cricket.add("Yes");
        cricket.add("no");
        cricket.add("maybe");
        cricket.add("none of them");

        List<String> football = new ArrayList<String>();
        football.add("Is 5 + 5 = 10 ?");
        football.add("yes");
        football.add("no");
        football.add("neither");
        football.add("all of them");

        List<String> basketball = new ArrayList<String>();
        basketball.add("why NASA keep lying to us?");
        basketball.add("true");
        basketball.add("false");
        basketball.add("");
        basketball.add("");

        expandableListDetail.put("Computer Features For The Second Group/ 2018-03-24", cricket);
        expandableListDetail.put("English For The First Group / 2018-03-24", football);
        expandableListDetail.put("Math For The First Group / 2018-03-24", basketball);
        return expandableListDetail;
    }
}