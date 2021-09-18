package com.ldt.cinematicket.data;

import android.os.AsyncTask;
import android.util.Log;

import com.ldt.cinematicket.util.HttpUtils;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.select.Elements;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.nio.charset.Charset;

public class LoadStringFromUrlAsyncTask extends AsyncTask<String, Void, String> {
    private static final String TAG="LoadString FromUrl";
    public interface OnLoadStringUrlListener {
        void onLoadComplete(String url,String content) ;
    }
    OnLoadStringUrlListener listener;

    private LoadStringFromUrlAsyncTask() {
        super();
    }

    public LoadStringFromUrlAsyncTask(OnLoadStringUrlListener listener) {
        this.listener = listener;
    }

    @Override
    protected void onPostExecute(String result) {
        if(listener!=null) listener.onLoadComplete(urlStr,result);
    }
    String urlStr;
    int code;
    @Override
    protected String doInBackground(String... urlStr) {

     //   String content = HttpUtils.getContents(urlStr[0]);
        String s="[";
        Document doc =null;// = Jsoup.parse(content);
        try {
            doc = Jsoup.parse(new URL(urlStr[0]),5000);
        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        if(doc==null) return null;
       // doc.charset(Charset.forName("UTF-8"));
        Elements elements = doc.select("div.list_item");
        Elements titleElements = elements.select("td.overview-top");// = doc.select("td.overview-top").select("h4");
        Elements imageElements = elements.select("div.image").select("img");// doc.select("div.image").select("img");
        Elements timeElements = elements.select("time");
        Elements detailElements = elements.select("div.outline");
        for(int i = 0; i < titleElements.size(); i++)  {
            //  s+= "["+titleElements.get(i).child(0).text()+"], image = ["+imageElements.get(i).absUrl("nsrc")+"], time = "+timeElements.get(i).text()+", content = \""+detailElements.get(i).text()+"\"\n";
            s+="{"+
                    "\"title\": \""+titleElements.get(i).child(0).text()+"\""+
                    ", \"img\": \""+imageElements.get(i).absUrl("nsrc")+"\""+
                    ", \"duration\": \""+timeElements.get(i).text()+"\""+
                    ", \"content\": \""+detailElements.get(i).text()+"\""+
                    "}";
            if(i!=titleElements.size()-1) s+=", ";
        }
        s+="]";
        return s;
        //if(true) return s;
//        HttpURLConnection urlConnection = null;
//        String result = "";
//        try {
//            URL url = new URL(urlStr[0]);
//            urlConnection = (HttpURLConnection) url.openConnection();
//            urlConnection.setConnectTimeout(10000);
//            urlConnection.setReadTimeout(10000);
//            Log.d(TAG, "doInBackground: wait response code");
//            code = urlConnection.getResponseCode();
//          //  code = 200;
//            Log.d(TAG, "doInBackground: received code = "+code);
//            if (code == 200) {
//                InputStream in = new BufferedInputStream(urlConnection.getInputStream());
//                if (in != null) {
//                    BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(in));
//                    String line = "";
//
//                    while ((line = bufferedReader.readLine()) != null)
//                        result += line;
//                }
//                in.close();
//            }
//
//            return result;
//        } catch (MalformedURLException e) {
//            Log.e(TAG, "doInBackground: "+e );
//            e.printStackTrace();
//        } catch (IOException e) {
//            e.printStackTrace();
//        } finally {
//            urlConnection.disconnect();
//        }
//        return result;
    }
}