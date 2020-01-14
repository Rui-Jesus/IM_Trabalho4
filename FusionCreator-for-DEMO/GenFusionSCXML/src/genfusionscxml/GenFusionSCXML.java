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
    
    //Single commands
    fg.Single(Speech.ADD_FAV, Output.ADD_FAV);
    fg.Single(Speech.OPEN_FAV, Output.OPEN_FAV);
    fg.Single(Speech.NEXT_TAB, Output.NEXT_TAB);
    fg.Single(Speech.PREVIOUS_TAB, Output.PREVIOUS_TAB);
    
    //Complementary commands
    fg.Complementary(SecondMod.ZOOM_IN, Speech.QUANTITY_TIMES, Output.ZOOM_IN);
    fg.Complementary(SecondMod.ZOOM_OUT, Speech.QUANTITY_TIMES, Output.ZOOM_OUT);
    fg.Complementary(SecondMod.SCROLL_UP, Speech.QUANTITY_TIMES, Output.SCROLL_UP);
    fg.Complementary(SecondMod.SCROLL_DOWN, Speech.QUANTITY_TIMES, Output.SCROLL_DOWN);
    
    // O search e o open link não aparecem aqui pois vão diretamente para o appGUI
    
    fg.Build("fusion.scxml");
    }
    
}
