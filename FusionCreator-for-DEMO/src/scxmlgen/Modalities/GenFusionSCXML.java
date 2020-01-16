/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package genfusionscxml;

import java.io.IOException;
import scxmlgen.Fusion.FusionGenerator;
import scxmlgen.Modalities.Output;
import scxmlgen.Modalities.Speech;
import scxmlgen.Modalities.SecondMod;

/**
 *
 * @author nunof
 */
public class GenFusionSCXML {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) throws IOException {

    FusionGenerator fg = new FusionGenerator();
    
    //Redundant commands
    fg.Redundancy(Speech.CLOSE_TAB, SecondMod.CLOSE_TAB, Output.CLOSE_TAB);
    fg.Redundancy(Speech.OPEN_TAB, SecondMod.OPEN_TAB, Output.OPEN_TAB);
    fg.Redundancy(Speech.OPEN_HISTORIC, SecondMod.OPEN_HISTORIC, Output.OPEN_HISTORIC);
    fg.Redundancy(Speech.MAXIMIZE, SecondMod.MAXIMIZE, Output.MAXIMIZE);
    fg.Redundancy(Speech.MINIMIZE, SecondMod.MINIMIZE, Output.MINIMIZE);
    
    //Single commands - Não precisam de estar aqui tal como o search
    fg.Single(Speech.ADD_FAV, Output.ADD_FAV);
    fg.Single(Speech.OPEN_FAV, Output.OPEN_FAV);
    fg.Single(Speech.NEXT_TAB, Output.NEXT_TAB);
    fg.Single(Speech.PREVIOUS_TAB, Output.PREVIOUS_TAB);
    fg.Single(Speech.CLOSE_TAB, Output.CLOSE_TAB);


    //Complementary commands
    fg.Sequence(SecondMod.ZOOM_IN, Speech.TIMES_2, Output.ZOOM_IN_2);
        fg.Sequence(SecondMod.ZOOM_IN, Speech.TIMES_TOTAL, Output.ZOOM_IN_TOTAL);
    fg.Sequence(SecondMod.ZOOM_IN, Speech.TIMES_3, Output.ZOOM_IN_3);
    fg.Sequence(Speech.TIMES_2, SecondMod.ZOOM_IN, Output.ZOOM_IN_2);
    fg.Sequence(Speech.TIMES_3, SecondMod.ZOOM_IN, Output.ZOOM_IN_3);
    fg.Sequence(Speech.TIMES_TOTAL, SecondMod.ZOOM_IN, Output.ZOOM_IN_TOTAL);

    fg.Single(SecondMod.ZOOM_IN, Output.ZOOM_IN);
    fg.Single(Speech.ZOOM_IN, Output.ZOOM_IN);

    fg.Sequence(SecondMod.ZOOM_OUT, Speech.TIMES_2, Output.ZOOM_OUT_2);
    fg.Sequence(SecondMod.ZOOM_OUT, Speech.TIMES_3, Output.ZOOM_OUT_3);
        fg.Sequence(SecondMod.ZOOM_OUT, Speech.TIMES_TOTAL, Output.ZOOM_OUT_TOTAL);
    fg.Sequence(Speech.TIMES_2, SecondMod.ZOOM_OUT, Output.ZOOM_OUT_2);
    fg.Sequence(Speech.TIMES_3, SecondMod.ZOOM_OUT, Output.ZOOM_OUT_3);
        fg.Sequence(Speech.TIMES_TOTAL, SecondMod.ZOOM_OUT, Output.ZOOM_OUT_TOTAL);
    fg.Single(SecondMod.ZOOM_OUT, Output.ZOOM_OUT);
    fg.Single(Speech.ZOOM_OUT, Output.ZOOM_OUT);

        //Complementary commands
    fg.Sequence(SecondMod.SCROLL_UP, Speech.TIMES_2, Output.SCROLL_UP_2);
        fg.Sequence(SecondMod.SCROLL_UP, Speech.TIMES_TOTAL, Output.SCROLL_UP_TOTAL);
    fg.Sequence(SecondMod.SCROLL_UP, Speech.TIMES_3, Output.SCROLL_UP_3);
    fg.Sequence(Speech.TIMES_2, SecondMod.SCROLL_UP, Output.SCROLL_UP_2);
    fg.Sequence(Speech.TIMES_3, SecondMod.SCROLL_UP, Output.SCROLL_UP_3);
    fg.Sequence(Speech.TIMES_TOTAL, SecondMod.SCROLL_UP, Output.SCROLL_UP_TOTAL);
    fg.Single(SecondMod.SCROLL_UP, Output.SCROLL_UP);
    fg.Single(Speech.SCROLL_UP, Output.SCROLL_UP);

    fg.Sequence(SecondMod.SCROLL_DOWN, Speech.TIMES_2, Output.SCROLL_DOWN_2);
    fg.Sequence(SecondMod.SCROLL_DOWN, Speech.TIMES_3, Output.SCROLL_DOWN_3);
    fg.Sequence(SecondMod.SCROLL_DOWN, Speech.TIMES_TOTAL, Output.SCROLL_DOWN_TOTAL);
    fg.Sequence(Speech.TIMES_2, SecondMod.SCROLL_DOWN, Output.SCROLL_DOWN_2);
    fg.Sequence(Speech.TIMES_3, SecondMod.SCROLL_DOWN, Output.SCROLL_DOWN_3);
    fg.Sequence(Speech.TIMES_TOTAL, SecondMod.SCROLL_DOWN, Output.SCROLL_DOWN_TOTAL);
    fg.Single(SecondMod.SCROLL_DOWN, Output.SCROLL_DOWN);
    fg.Single(Speech.SCROLL_DOWN, Output.SCROLL_DOWN);

    fg.Build("fusion.scxml");
    }
    
}
