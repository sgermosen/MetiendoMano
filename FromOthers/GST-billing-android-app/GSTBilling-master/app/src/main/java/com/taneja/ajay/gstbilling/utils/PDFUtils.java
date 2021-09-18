package com.taneja.ajay.gstbilling.utils;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.drawable.Drawable;
import android.graphics.pdf.PdfDocument;
import android.net.Uri;
import android.os.Build;
import android.os.Environment;
import android.support.annotation.RequiresApi;
import android.util.DisplayMetrics;
import android.view.Display;
import android.view.View;
import android.view.WindowManager;
import android.widget.Toast;

import com.taneja.ajay.gstbilling.SavePDFActivity;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;

/**
 * Created by Ajay on 8/18/2017.
 */

public class PDFUtils {

    private static int PAGE_WIDTH = 210*8;
    private static int PAGE_HEIGHT = 297*8;

    PdfDocument document;
    int pageNumber;

    @RequiresApi(api = Build.VERSION_CODES.KITKAT)
    public PDFUtils() {
        document = new PdfDocument();
        pageNumber = 1;
    }

    @RequiresApi(api = Build.VERSION_CODES.KITKAT)
    public void createPdf(Context context, String fileName){

        // write the document content
        File folder = new File(Environment.getExternalStorageDirectory() + File.separator + "BillingPDFs");
        folder.mkdirs();
        String targetPdf = fileName + ".pdf";
        File filePath = new File(folder + File.separator + targetPdf);
        try {
            document.writeTo(new FileOutputStream(filePath));
            Toast.makeText(context, "File saved : " + filePath.toString(), Toast.LENGTH_LONG).show();
        } catch (IOException e) {
            e.printStackTrace();
            Toast.makeText(context, "Something wrong: " + e.toString(), Toast.LENGTH_LONG).show();
        }

        // close the document
        document.close();

        //open the saved pdf file
        Uri uri = Uri.fromFile(filePath);
        Intent intent = new Intent(Intent.ACTION_VIEW);
        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
        intent.setDataAndType(uri, "application/pdf");
        context.startActivity(intent);
    }

    @RequiresApi(api = Build.VERSION_CODES.KITKAT)
    public void addPageToPDF(View printView){

        Bitmap bitmap = getBitmapFromView(printView);

        PdfDocument.PageInfo pageInfo = new PdfDocument.PageInfo.Builder(PAGE_WIDTH, PAGE_HEIGHT, pageNumber).create();
        PdfDocument.Page page = document.startPage(pageInfo);

        Canvas canvas = page.getCanvas();


        Paint paint = new Paint();
        canvas.drawPaint(paint);


        bitmap = Bitmap.createScaledBitmap(bitmap, PAGE_WIDTH, PAGE_HEIGHT, true);

        paint.setColor(Color.BLUE);
        canvas.drawBitmap(bitmap, 0, 0 , null);
        document.finishPage(page);

        pageNumber++;
    }

    public static Bitmap getBitmapFromView(View view) {
        Bitmap returnedBitmap = Bitmap.createBitmap(view.getWidth(), view.getHeight(),Bitmap.Config.ARGB_8888);
        Canvas canvas = new Canvas(returnedBitmap);
        Drawable bgDrawable =view.getBackground();
        if (bgDrawable!=null)
            bgDrawable.draw(canvas);
        else
            canvas.drawColor(Color.WHITE);
        view.draw(canvas);
        return returnedBitmap;
    }

}
