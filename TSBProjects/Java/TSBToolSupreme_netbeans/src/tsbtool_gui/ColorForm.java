/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package tsbtool_gui;

import javax.swing.*;
import java.awt.*;

/// <summary>
/// Summary description for ColorForm2.
/// </summary>
public class ColorForm extends javax.swing.JDialog {

    private int labelFontSize = 12;
    private JLabel[] m00Panels = new JLabel[16];
    private JLabel[] m10Panels = new JLabel[16];
    private JLabel[] m20Panels = new JLabel[16];
    private JLabel[] m30Panels = new JLabel[16];
    private java.awt.event.MouseAdapter mMouseListener =
            new java.awt.event.MouseAdapter() {
        public void mouseClicked(java.awt.event.MouseEvent evt) {
            colorPanelClicked(evt);
        }
    };

    public ColorForm(JFrame parent, boolean modal) {
        super(parent, modal);
        initComponents();
        try{
            setIconImage(MainGUI.GetImage("/tsbtool_gui/icons/49ers.PNG", this));
        }
        catch(Exception e){
        }
        AddPanelsToArrays();
        ColorizePanels();

        for (int i = 0; i < m00Panels.length; i++) {
            m00Panels[i].setOpaque(true);
            m10Panels[i].setOpaque(true);
            m20Panels[i].setOpaque(true);
            m30Panels[i].setOpaque(true);
        }
    }
    //00h
    private int[] mZeroHRow = {
        124, 124, 124, 0, 0, 252, 0, 0, 188, 68, 40, 188,
        148, 0, 132, 168, 0, 32, 168, 16, 0, 136, 20, 0,
        80, 48, 0, 0, 120, 0, 0, 104, 0, 0, 88, 0,
        0, 64, 88, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    //10h
    private int[] mTenHRow = {
        188, 188, 188, 0, 120, 248, 0, 88, 248, 104, 68, 252,
        216, 0, 204, 228, 0, 88, 248, 56, 0, 228, 92, 16,
        172, 124, 0, 0, 184, 0, 0, 168, 0, 0, 168, 68,
        0, 136, 136, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    //20h
    private int[] mTwentyHRow = {
        248, 248, 248, 60, 188, 252, 104, 136, 252, 152, 120, 248,
        248, 120, 248, 248, 88, 152, 248, 120, 88, 252, 160, 68,
        248, 184, 0, 184, 248, 24, 88, 216, 84, 88, 248, 152,
        0, 232, 216, 120, 120, 120, 0, 0, 0, 0, 0, 0};
    //30h
    private int[] mThirtyHRow = {
        252, 252, 252, 164, 228, 252, 184, 184, 248, 216, 184, 248,
        248, 184, 248, 248, 164, 192, 240, 208, 176, 252, 224, 168,
        248, 216, 120, 216, 248, 120, 184, 248, 184, 184, 248, 216,
        0, 252, 252, 248, 216, 248, 0, 0, 0, 0, 0, 0};
    private boolean mCanceled = false;

    public boolean getCanceled() {
        return mCanceled;
    }
    private Color mCurrentColor;

    public Color GetColor(String hexStr) {
        Color ret = null;
        if (hexStr != null && hexStr.length() == 2) {
            //                int left = hexStr[0] - 48;
            //                int top  = hexStr[1] - 48;
            int num = Integer.parseInt(hexStr, 16);
            int left = num >> 4;
            int top = num & 0x0F;
            JLabel[] row = null;

            if (left == 0) {
                row = m00Panels;
            } else if (left == 1) {
                row = m10Panels;
            } else if (left == 2) {
                row = m20Panels;
            } else if (left == 3) {
                row = m30Panels;
            }

            if (row != null && top < row.length) {
                ret = row[top].getBackground();
            }
        }
        return ret;
    }

    /// <summary>
    /// The last color chosen by the user.
    /// </summary>
    public Color getCurrentColor() {
        return mCurrentColor;
    }

    public void setCurrentColor(Color value) {
        if (mCurrentColor != value) {
            mCurrentColor = value;
            mCurrentColorPanel.setBackground(mCurrentColor);
        }
    }
    private String mCurrentColorString = "";
    private javax.swing.JTextField mDebugBox;
    private javax.swing.JLabel label1;
    private javax.swing.JLabel label2;
    private javax.swing.JLabel label3;
    private javax.swing.JLabel label4;
    private javax.swing.JLabel label5;

    /// <summary>
    /// The NES Color representation of the current color as a String .
    /// Like "00" or "30"
    /// </summary>
    public String getCurrentColorString() {
        return mCurrentColorString;
    }

    public void setCurrentColorString(String value) {
        if (!mCurrentColorString.equals(value)) {
            mCurrentColorString = value;
            mCurrentColorTextBox.setText(mCurrentColorString);
            setCurrentColor(GetColor(value));
        }
    }

    private void AddPanelsToArrays() {
        m00Panels[0] = panel0;
        m00Panels[1] = panel1;
        m00Panels[2] = panel2;
        m00Panels[3] = panel3;
        m00Panels[4] = panel4;
        m00Panels[5] = panel5;
        m00Panels[6] = panel6;
        m00Panels[7] = panel7;
        m00Panels[8] = panel8;
        m00Panels[9] = panel9;
        m00Panels[10] = panel10;
        m00Panels[11] = panel11;
        m00Panels[12] = panel12;
        m00Panels[13] = panel13;
        m00Panels[14] = panel14;
        m00Panels[15] = panel15;

        m10Panels[0] = panel16;
        m10Panels[1] = panel17;
        m10Panels[2] = panel18;
        m10Panels[3] = panel19;
        m10Panels[4] = panel20;
        m10Panels[5] = panel21;
        m10Panels[6] = panel22;
        m10Panels[7] = panel23;
        m10Panels[8] = panel24;
        m10Panels[9] = panel25;
        m10Panels[10] = panel26;
        m10Panels[11] = panel27;
        m10Panels[12] = panel28;
        m10Panels[13] = panel29;
        m10Panels[14] = panel30;
        m10Panels[15] = panel31;

        m20Panels[0] = panel32;
        m20Panels[1] = panel33;
        m20Panels[2] = panel34;
        m20Panels[3] = panel35;
        m20Panels[4] = panel36;
        m20Panels[5] = panel37;
        m20Panels[6] = panel38;
        m20Panels[7] = panel39;
        m20Panels[8] = panel40;
        m20Panels[9] = panel41;
        m20Panels[10] = panel42;
        m20Panels[11] = panel43;
        m20Panels[12] = panel44;
        m20Panels[13] = panel45;
        m20Panels[14] = panel46;
        m20Panels[15] = panel47;

        m30Panels[0] = panel48;
        m30Panels[1] = panel49;
        m30Panels[2] = panel50;
        m30Panels[3] = panel51;
        m30Panels[4] = panel52;
        m30Panels[5] = panel53;
        m30Panels[6] = panel54;
        m30Panels[7] = panel55;
        m30Panels[8] = panel56;
        m30Panels[9] = panel57;
        m30Panels[10] = panel58;
        m30Panels[11] = panel59;
        m30Panels[12] = panel60;
        m30Panels[13] = panel61;
        m30Panels[14] = panel62;
        m30Panels[15] = panel63;
    }

    private void ColorizePanels() {
        int panelNum = 0;
        int r, g, b;
        for (int i = 0; i < mZeroHRow.length; i += 3) {
            r = mZeroHRow[i];
            g = mZeroHRow[i + 1];
            b = mZeroHRow[i + 2];
            m00Panels[panelNum].setBackground(new java.awt.Color(r, g, b));
            panelNum++;
        }

        panelNum = 0;
        for (int i = 0; i < mTenHRow.length; i += 3) {
            r = mTenHRow[i];
            g = mTenHRow[i + 1];
            b = mTenHRow[i + 2];
            m10Panels[panelNum].setBackground(new java.awt.Color(r, g, b));
            //m10Panels[panelNum].BackColor = Color.FromArgb(mTenHRow[i],mTenHRow[1+1],mTenHRow[i+2]);
            panelNum++;
        }

        panelNum = 0;
        for (int i = 0; i < mTwentyHRow.length; i += 3) {
            r = mTwentyHRow[i];
            g = mTwentyHRow[i + 1];
            b = mTwentyHRow[i + 2];
            m20Panels[panelNum].setBackground(new java.awt.Color(r, g, b));
            //m20Panels[panelNum].BackColor = Color.FromArgb(mTwentyHRow[i],mTwentyHRow[1+1],mTwentyHRow[i+2]);
            panelNum++;
        }

        panelNum = 0;
        for (int i = 0; i < mThirtyHRow.length; i += 3) {
            r = mThirtyHRow[i];
            g = mThirtyHRow[i + 1];
            b = mThirtyHRow[i + 2];
            m30Panels[panelNum].setBackground(new java.awt.Color(r, g, b));
            //m30Panels[panelNum].BackColor = Color.FromArgb(mThirtyHRow[i],mThirtyHRow[1+1],mThirtyHRow[i+2]);
            panelNum++;
        }

    }

    private String GetColorString(JLabel p) {
        String ret = null;
        for (int i = 0; i < m00Panels.length; i++) {
            if (m00Panels[i] == p) {
                ret = String.format("0%X", i);
                return ret;
            }
        }
        for (int i = 0; i < m10Panels.length; i++) {
            if (m10Panels[i] == p) {
                ret = String.format("1%X", i);
                return ret;
            }
        }
        for (int i = 0; i < m20Panels.length; i++) {
            if (m20Panels[i] == p) {
                ret = String.format("2%X", i);
                return ret;
            }
        }
        for (int i = 0; i < m30Panels.length; i++) {
            if (m30Panels[i] == p) {
                ret = String.format("3%X", i);
                return ret;
            }
        }
        return ret;
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">                          
    private void initComponents() {
        getContentPane().setLayout(null);
        this.panel0 = new javax.swing.JLabel();
        this.panel1 = new javax.swing.JLabel();
        this.panel2 = new javax.swing.JLabel();
        this.panel3 = new javax.swing.JLabel();
        this.panel4 = new javax.swing.JLabel();
        this.panel5 = new javax.swing.JLabel();
        this.panel6 = new javax.swing.JLabel();
        this.panel7 = new javax.swing.JLabel();
        this.panel8 = new javax.swing.JLabel();
        this.panel9 = new javax.swing.JLabel();
        this.panel10 = new javax.swing.JLabel();
        this.panel11 = new javax.swing.JLabel();
        this.panel12 = new javax.swing.JLabel();
        this.panel13 = new javax.swing.JLabel();
        this.panel14 = new javax.swing.JLabel();
        this.panel15 = new javax.swing.JLabel();
        this.panel16 = new javax.swing.JLabel();
        this.panel17 = new javax.swing.JLabel();
        this.panel18 = new javax.swing.JLabel();
        this.panel19 = new javax.swing.JLabel();
        this.panel21 = new javax.swing.JLabel();
        this.panel22 = new javax.swing.JLabel();
        this.panel23 = new javax.swing.JLabel();
        this.panel20 = new javax.swing.JLabel();
        this.panel24 = new javax.swing.JLabel();
        this.panel25 = new javax.swing.JLabel();
        this.panel26 = new javax.swing.JLabel();
        this.panel27 = new javax.swing.JLabel();
        this.panel30 = new javax.swing.JLabel();
        this.panel31 = new javax.swing.JLabel();
        this.panel29 = new javax.swing.JLabel();
        this.panel28 = new javax.swing.JLabel();
        this.panel32 = new javax.swing.JLabel();
        this.panel33 = new javax.swing.JLabel();
        this.panel34 = new javax.swing.JLabel();
        this.panel35 = new javax.swing.JLabel();
        this.panel37 = new javax.swing.JLabel();
        this.panel38 = new javax.swing.JLabel();
        this.panel39 = new javax.swing.JLabel();
        this.panel36 = new javax.swing.JLabel();
        this.panel40 = new javax.swing.JLabel();
        this.panel41 = new javax.swing.JLabel();
        this.panel42 = new javax.swing.JLabel();
        this.panel43 = new javax.swing.JLabel();
        this.panel46 = new javax.swing.JLabel();
        this.panel47 = new javax.swing.JLabel();
        this.panel45 = new javax.swing.JLabel();
        this.panel44 = new javax.swing.JLabel();
        this.panel57 = new javax.swing.JLabel();
        this.panel58 = new javax.swing.JLabel();
        this.panel48 = new javax.swing.JLabel();
        this.panel49 = new javax.swing.JLabel();
        this.panel50 = new javax.swing.JLabel();
        this.panel51 = new javax.swing.JLabel();
        this.panel53 = new javax.swing.JLabel();
        this.panel54 = new javax.swing.JLabel();
        this.panel55 = new javax.swing.JLabel();
        this.panel52 = new javax.swing.JLabel();
        this.panel56 = new javax.swing.JLabel();
        this.panel59 = new javax.swing.JLabel();
        this.panel62 = new javax.swing.JLabel();
        this.panel63 = new javax.swing.JLabel();
        this.panel61 = new javax.swing.JLabel();
        this.panel60 = new javax.swing.JLabel();
        this.mOKButton = new javax.swing.JButton();
        this.mCancelButton = new javax.swing.JButton();
        this.mCurrentColorTextBox = new javax.swing.JTextField();
        this.mCurrentColorPanel = new javax.swing.JLabel();
        this.mDebugBox = new javax.swing.JTextField();
        this.label1 = new javax.swing.JLabel();
        this.label2 = new javax.swing.JLabel();
        this.label3 = new javax.swing.JLabel();
        this.label4 = new javax.swing.JLabel();
        this.label5 = new javax.swing.JLabel();
        // 
        // panel0
        // 
        this.panel0.setBackground(new java.awt.Color(153, 153, 255));
        this.panel0.setLocation(24, 24);
        // Name="panel0";
        this.panel0.setSize(25, 25);
        // = 0;
        this.panel0.addMouseListener(mMouseListener);
        // 
        // panel1
        // 
        this.panel1.setBackground(new java.awt.Color(153, 153, 255));
        this.panel1.setLocation(56, 24);
        // Name="panel1";
        this.panel1.setSize(25, 25);
        // = 1;
        this.panel1.addMouseListener(mMouseListener);
        // 
        // panel2
        // 
        this.panel2.setBackground(new java.awt.Color(153, 153, 255));
        this.panel2.setLocation(88, 24);
        // Name="panel2";
        this.panel2.setSize(25, 25);
        // = 2;
        this.panel2.addMouseListener(mMouseListener);
        // 
        // panel3
        // 
        this.panel3.setBackground(new java.awt.Color(153, 153, 255));
        this.panel3.setLocation(120, 24);
        // Name="panel3";
        this.panel3.setSize(25, 25);
        // = 3;
        this.panel3.addMouseListener(mMouseListener);
        // 
        // panel4
        // 
        this.panel4.setBackground(new java.awt.Color(153, 153, 255));
        this.panel4.setLocation(152, 24);
        // Name="panel4";
        this.panel4.setSize(25, 25);
        // = 4;
        this.panel4.addMouseListener(mMouseListener);
        // 
        // panel5
        // 
        this.panel5.setBackground(new java.awt.Color(153, 153, 255));
        this.panel5.setLocation(184, 24);
        // Name="panel5";
        this.panel5.setSize(25, 25);
        // = 5;
        this.panel5.addMouseListener(mMouseListener);
        // 
        // panel6
        // 
        this.panel6.setBackground(new java.awt.Color(153, 153, 255));
        this.panel6.setLocation(216, 24);
        // Name="panel6";
        this.panel6.setSize(25, 25);
        // = 6;
        this.panel6.addMouseListener(mMouseListener);
        // 
        // panel7
        // 
        this.panel7.setBackground(new java.awt.Color(153, 153, 255));
        this.panel7.setLocation(248, 24);
        // Name="panel7";
        this.panel7.setSize(25, 25);
        // = 7;
        this.panel7.addMouseListener(mMouseListener);
        // 
        // panel8
        // 
        this.panel8.setBackground(new java.awt.Color(153, 153, 255));
        this.panel8.setLocation(280, 24);
        // Name="panel8";
        this.panel8.setSize(25, 25);
        // = 4;
        this.panel8.addMouseListener(mMouseListener);
        // 
        // panel9
        // 
        this.panel9.setBackground(new java.awt.Color(153, 153, 255));
        this.panel9.setLocation(312, 24);
        // Name="panel9";
        this.panel9.setSize(25, 25);
        // = 5;
        this.panel9.addMouseListener(mMouseListener);
        // 
        // panel10
        // 
        this.panel10.setBackground(new java.awt.Color(153, 153, 255));
        this.panel10.setLocation(344, 24);
        // Name="panel10";
        this.panel10.setSize(25, 25);
        // = 6;
        this.panel10.addMouseListener(mMouseListener);
        // 
        // panel11
        // 
        this.panel11.setBackground(new java.awt.Color(153, 153, 255));
        this.panel11.setLocation(376, 24);
        // Name="panel11";
        this.panel11.setSize(25, 25);
        // = 7;
        this.panel11.addMouseListener(mMouseListener);
        // 
        // panel12
        // 
        this.panel12.setBackground(new java.awt.Color(153, 153, 255));
        this.panel12.setLocation(408, 24);
        // Name="panel12";
        this.panel12.setSize(25, 25);
        // = 8;
        this.panel12.addMouseListener(mMouseListener);
        // 
        // panel13
        // 
        this.panel13.setBackground(new java.awt.Color(153, 153, 255));
        this.panel13.setLocation(440, 24);
        // Name="panel13";
        this.panel13.setSize(25, 25);
        // = 9;
        this.panel13.addMouseListener(mMouseListener);
        // 
        // panel14
        // 
        this.panel14.setBackground(new java.awt.Color(153, 153, 255));
        this.panel14.setLocation(472, 24);
        // Name="panel14";
        this.panel14.setSize(25, 25);
        // = 10;
        this.panel14.addMouseListener(mMouseListener);
        // 
        // panel15
        // 
        this.panel15.setBackground(new java.awt.Color(153, 153, 255));
        this.panel15.setLocation(504, 24);
        // Name="panel15";
        this.panel15.setSize(25, 25);
        // = 11;
        this.panel15.addMouseListener(mMouseListener);
        // 
        // panel16
        // 
        this.panel16.setBackground(new java.awt.Color(153, 153, 255));
        this.panel16.setLocation(24, 56);
        // Name="panel16";
        this.panel16.setSize(25, 25);
        // = 12;
        this.panel16.addMouseListener(mMouseListener);
        // 
        // panel17
        // 
        this.panel17.setBackground(new java.awt.Color(153, 153, 255));
        this.panel17.setLocation(56, 56);
        // Name="panel17";
        this.panel17.setSize(25, 25);
        // = 13;
        this.panel17.addMouseListener(mMouseListener);
        // 
        // panel18
        // 
        this.panel18.setBackground(new java.awt.Color(153, 153, 255));
        this.panel18.setLocation(88, 56);
        // Name="panel18";
        this.panel18.setSize(25, 25);
        // = 14;
        this.panel18.addMouseListener(mMouseListener);
        // 
        // panel19
        // 
        this.panel19.setBackground(new java.awt.Color(153, 153, 255));
        this.panel19.setLocation(120, 56);
        // Name="panel19";
        this.panel19.setSize(25, 25);
        // = 15;
        this.panel19.addMouseListener(mMouseListener);
        // 
        // panel21
        // 
        this.panel21.setBackground(new java.awt.Color(153, 153, 255));
        this.panel21.setLocation(184, 56);
        // Name="panel21";
        this.panel21.setSize(25, 25);
        // = 18;
        this.panel21.addMouseListener(mMouseListener);
        // 
        // panel22
        // 
        this.panel22.setBackground(new java.awt.Color(153, 153, 255));
        this.panel22.setLocation(216, 56);
        // Name="panel22";
        this.panel22.setSize(25, 25);
        // = 21;
        this.panel22.addMouseListener(mMouseListener);
        // 
        // panel23
        // 
        this.panel23.setBackground(new java.awt.Color(153, 153, 255));
        this.panel23.setLocation(248, 56);
        // Name="panel23";
        this.panel23.setSize(25, 25);
        // = 23;
        this.panel23.addMouseListener(mMouseListener);
        // 
        // panel20
        // 
        this.panel20.setBackground(new java.awt.Color(153, 153, 255));
        this.panel20.setLocation(152, 56);
        // Name="panel20";
        this.panel20.setSize(25, 25);
        // = 16;
        this.panel20.addMouseListener(mMouseListener);
        // 
        // panel24
        // 
        this.panel24.setBackground(new java.awt.Color(153, 153, 255));
        this.panel24.setLocation(280, 56);
        // Name="panel24";
        this.panel24.setSize(25, 25);
        // = 17;
        this.panel24.addMouseListener(mMouseListener);
        // 
        // panel25
        // 
        this.panel25.setBackground(new java.awt.Color(153, 153, 255));
        this.panel25.setLocation(312, 56);
        // Name="panel25";
        this.panel25.setSize(25, 25);
        // = 19;
        this.panel25.addMouseListener(mMouseListener);
        // 
        // panel26
        // 
        this.panel26.setBackground(new java.awt.Color(153, 153, 255));
        this.panel26.setLocation(344, 56);
        // Name="panel26";
        this.panel26.setSize(25, 25);
        // = 20;
        this.panel26.addMouseListener(mMouseListener);
        // 
        // panel27
        // 
        this.panel27.setBackground(new java.awt.Color(153, 153, 255));
        this.panel27.setLocation(376, 56);
        // Name="panel27";
        this.panel27.setSize(25, 25);
        // = 22;
        this.panel27.addMouseListener(mMouseListener);
        // 
        // panel30
        // 
        this.panel30.setBackground(new java.awt.Color(153, 153, 255));
        this.panel30.setLocation(472, 56);
        // Name="panel30";
        this.panel30.setSize(25, 25);
        // = 26;
        this.panel30.addMouseListener(mMouseListener);
        // 
        // panel31
        // 
        this.panel31.setBackground(new java.awt.Color(153, 153, 255));
        this.panel31.setLocation(504, 56);
        // Name="panel31";
        this.panel31.setSize(25, 25);
        // = 27;
        this.panel31.addMouseListener(mMouseListener);
        // 
        // panel29
        // 
        this.panel29.setBackground(new java.awt.Color(153, 153, 255));
        this.panel29.setLocation(440, 56);
        // Name="panel29";
        this.panel29.setSize(25, 25);
        // = 25;
        this.panel29.addMouseListener(mMouseListener);
        // 
        // panel28
        // 
        this.panel28.setBackground(new java.awt.Color(153, 153, 255));
        this.panel28.setLocation(408, 56);
        // Name="panel28";
        this.panel28.setSize(25, 25);
        // = 24;
        this.panel28.addMouseListener(mMouseListener);
        // 
        // panel32
        // 
        this.panel32.setBackground(new java.awt.Color(153, 153, 255));
        this.panel32.setLocation(24, 88);
        // Name="panel32";
        this.panel32.setSize(25, 25);
        // = 28;
        this.panel32.addMouseListener(mMouseListener);
        // 
        // panel33
        // 
        this.panel33.setBackground(new java.awt.Color(153, 153, 255));
        this.panel33.setLocation(56, 88);
        // Name="panel33";
        this.panel33.setSize(25, 25);
        // = 29;
        this.panel33.addMouseListener(mMouseListener);
        // 
        // panel34
        // 
        this.panel34.setBackground(new java.awt.Color(153, 153, 255));
        this.panel34.setLocation(88, 88);
        // Name="panel34";
        this.panel34.setSize(25, 25);
        // = 30;
        this.panel34.addMouseListener(mMouseListener);
        // 
        // panel35
        // 
        this.panel35.setBackground(new java.awt.Color(153, 153, 255));
        this.panel35.setLocation(120, 88);
        // Name="panel35";
        this.panel35.setSize(25, 25);
        // = 31;
        this.panel35.addMouseListener(mMouseListener);
        // 
        // panel37
        // 
        this.panel37.setBackground(new java.awt.Color(153, 153, 255));
        this.panel37.setLocation(184, 88);
        // Name="panel37";
        this.panel37.setSize(25, 25);
        // = 35;
        this.panel37.addMouseListener(mMouseListener);
        // 
        // panel38
        // 
        this.panel38.setBackground(new java.awt.Color(153, 153, 255));
        this.panel38.setLocation(216, 88);
        // Name="panel38";
        this.panel38.setSize(25, 25);
        // = 37;
        this.panel38.addMouseListener(mMouseListener);
        // 
        // panel39
        // 
        this.panel39.setBackground(new java.awt.Color(153, 153, 255));
        this.panel39.setLocation(248, 88);
        // Name="panel39";
        this.panel39.setSize(25, 25);
        // = 39;
        this.panel39.addMouseListener(mMouseListener);
        // 
        // panel36
        // 
        this.panel36.setBackground(new java.awt.Color(153, 153, 255));
        this.panel36.setLocation(152, 88);
        // Name="panel36";
        this.panel36.setSize(25, 25);
        // = 32;
        this.panel36.addMouseListener(mMouseListener);
        // 
        // panel40
        // 
        this.panel40.setBackground(new java.awt.Color(153, 153, 255));
        this.panel40.setLocation(280, 88);
        // Name="panel40";
        this.panel40.setSize(25, 25);
        // = 33;
        this.panel40.addMouseListener(mMouseListener);
        // 
        // panel41
        // 
        this.panel41.setBackground(new java.awt.Color(153, 153, 255));
        this.panel41.setLocation(312, 88);
        // Name="panel41";
        this.panel41.setSize(25, 25);
        // = 34;
        this.panel41.addMouseListener(mMouseListener);
        // 
        // panel42
        // 
        this.panel42.setBackground(new java.awt.Color(153, 153, 255));
        this.panel42.setLocation(344, 88);
        // Name="panel42";
        this.panel42.setSize(25, 25);
        // = 36;
        this.panel42.addMouseListener(mMouseListener);
        // 
        // panel43
        // 
        this.panel43.setBackground(new java.awt.Color(153, 153, 255));
        this.panel43.setLocation(376, 88);
        // Name="panel43";
        this.panel43.setSize(25, 25);
        // = 38;
        this.panel43.addMouseListener(mMouseListener);
        // 
        // panel46
        // 
        this.panel46.setBackground(new java.awt.Color(153, 153, 255));
        this.panel46.setLocation(472, 88);
        // Name="panel46";
        this.panel46.setSize(25, 25);
        // = 42;
        this.panel46.addMouseListener(mMouseListener);
        // 
        // panel47
        // 
        this.panel47.setBackground(new java.awt.Color(153, 153, 255));
        this.panel47.setLocation(504, 88);
        // Name="panel47";
        this.panel47.setSize(25, 25);
        // = 43;
        this.panel47.addMouseListener(mMouseListener);
        // 
        // panel45
        // 
        this.panel45.setBackground(new java.awt.Color(153, 153, 255));
        this.panel45.setLocation(440, 88);
        // Name="panel45";
        this.panel45.setSize(25, 25);
        // = 41;
        this.panel45.addMouseListener(mMouseListener);
        // 
        // panel44
        // 
        this.panel44.setBackground(new java.awt.Color(153, 153, 255));
        this.panel44.setLocation(408, 88);
        // Name="panel44";
        this.panel44.setSize(25, 25);
        // = 40;
        this.panel44.addMouseListener(mMouseListener);
        // 
        // panel57
        // 
        this.panel57.setBackground(new java.awt.Color(153, 153, 255));
        this.panel57.setLocation(312, 120);
        // Name="panel57";
        this.panel57.setSize(25, 25);
        // = 51;
        this.panel57.addMouseListener(mMouseListener);
        // 
        // panel58
        // 
        this.panel58.setBackground(new java.awt.Color(153, 153, 255));
        this.panel58.setLocation(344, 120);
        // Name="panel58";
        this.panel58.setSize(25, 25);
        // = 52;
        this.panel58.addMouseListener(mMouseListener);
        // 
        // panel48
        // 
        this.panel48.setBackground(new java.awt.Color(153, 153, 255));
        this.panel48.setLocation(24, 120);
        // Name="panel48";
        this.panel48.setSize(25, 25);
        // = 44;
        this.panel48.addMouseListener(mMouseListener);
        // 
        // panel49
        // 
        this.panel49.setBackground(new java.awt.Color(153, 153, 255));
        this.panel49.setLocation(56, 120);
        // Name="panel49";
        this.panel49.setSize(25, 25);
        // = 45;
        this.panel49.addMouseListener(mMouseListener);
        // 
        // panel50
        // 
        this.panel50.setBackground(new java.awt.Color(153, 153, 255));
        this.panel50.setLocation(88, 120);
        // Name="panel50";
        this.panel50.setSize(25, 25);
        // = 46;
        this.panel50.addMouseListener(mMouseListener);
        // 
        // panel51
        // 
        this.panel51.setBackground(new java.awt.Color(153, 153, 255));
        this.panel51.setLocation(120, 120);
        // Name="panel51";
        this.panel51.setSize(25, 25);
        // = 47;
        this.panel51.addMouseListener(mMouseListener);
        // 
        // panel53
        // 
        this.panel53.setBackground(new java.awt.Color(153, 153, 255));
        this.panel53.setLocation(184, 120);
        // Name="panel53";
        this.panel53.setSize(25, 25);
        // = 50;
        this.panel53.addMouseListener(mMouseListener);
        // 
        // panel54
        // 
        this.panel54.setBackground(new java.awt.Color(153, 153, 255));
        this.panel54.setLocation(216, 120);
        // Name="panel54";
        this.panel54.setSize(25, 25);
        // = 53;
        this.panel54.addMouseListener(mMouseListener);
        // 
        // panel55
        // 
        this.panel55.setBackground(new java.awt.Color(153, 153, 255));
        this.panel55.setLocation(248, 120);
        // Name="panel55";
        this.panel55.setSize(25, 25);
        // = 55;
        this.panel55.addMouseListener(mMouseListener);
        // 
        // panel52
        // 
        this.panel52.setBackground(new java.awt.Color(153, 153, 255));
        this.panel52.setLocation(152, 120);
        // Name="panel52";
        this.panel52.setSize(25, 25);
        // = 48;
        this.panel52.addMouseListener(mMouseListener);
        // 
        // panel56
        // 
        this.panel56.setBackground(new java.awt.Color(153, 153, 255));
        this.panel56.setLocation(280, 120);
        // Name="panel56";
        this.panel56.setSize(25, 25);
        // = 49;
        this.panel56.addMouseListener(mMouseListener);
        // 
        // panel59
        // 
        this.panel59.setBackground(new java.awt.Color(153, 153, 255));
        this.panel59.setLocation(376, 120);
        // Name="panel59";
        this.panel59.setSize(25, 25);
        // = 54;
        this.panel59.addMouseListener(mMouseListener);
        // 
        // panel62
        // 
        this.panel62.setBackground(new java.awt.Color(153, 153, 255));
        this.panel62.setLocation(472, 120);
        // Name="panel62";
        this.panel62.setSize(25, 25);
        // = 58;
        this.panel62.addMouseListener(mMouseListener);
        // 
        // panel63
        // 
        this.panel63.setBackground(new java.awt.Color(153, 153, 255));
        this.panel63.setLocation(504, 120);
        // Name="panel63";
        this.panel63.setSize(25, 25);
        // = 59;
        this.panel63.addMouseListener(mMouseListener);
        // 
        // panel61
        // 
        this.panel61.setBackground(new java.awt.Color(153, 153, 255));
        this.panel61.setLocation(440, 120);
        // Name="panel61";
        this.panel61.setSize(25, 25);
        // = 57;
        this.panel61.addMouseListener(mMouseListener);
        // 
        // panel60
        // 
        this.panel60.setBackground(new java.awt.Color(153, 153, 255));
        this.panel60.setLocation(408, 120);
        // Name="panel60";
        this.panel60.setSize(25, 25);
        // = 56;
        this.panel60.addMouseListener(mMouseListener);
        // 
        // mOKButton
        // 
        this.mOKButton.setLocation(368, 168);
        //this.mOKButton.Name = "mOKButton";
        this.mOKButton.setSize(75, 23);
        ////= 60;
        this.mOKButton.setText("OK");
        this.mOKButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mOKButtonActionPerformed(evt);
            }
        });
        // 
        // mCancelButton
        // 
        this.mCancelButton.setLocation(456, 168);
        //this.mCancelButton.Name = "mCancelButton";
        this.mCancelButton.setSize(75, 23);
        //this.mCancelButton.TabIndex = 61;
        this.mCancelButton.setText("Cancel");
        this.mCancelButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                mCancelButtonActionPerformed(evt);
            }
        });
        // 
        // mCurrentColorTextBox
        // 
        //this.mCurrentColorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        //this.mCurrentColorTextBox.setFont(new java.awt.Font("Courier New", 0, 12)); // NOI18N
        this.mCurrentColorTextBox.setBackground(new java.awt.Color(153, 153, 255));;
        this.mCurrentColorTextBox.setLocation(24, 160);
        //this.mCurrentColorTextBox.Name = "mCurrentColorTextBox";
        this.mCurrentColorTextBox.setSize(31, 26);
        //this.mCurrentColorTextBox.TabIndex = 62;
        this.mCurrentColorTextBox.setText("00");
        // 
        // mCurrentColorPanel
        // 
        this.mCurrentColorPanel.setOpaque(true);
        this.mCurrentColorPanel.setBackground(new java.awt.Color(153, 153, 255));
        this.mCurrentColorPanel.setLocation(72, 160);
        //this.mCurrentColorPanel.Name = "mCurrentColorPanel";
        this.mCurrentColorPanel.setSize(25, 25);
        //= 47;
        // 
        // mDebugBox
        // 
        this.mDebugBox.setLocation(144, 160);
        //this.mDebugBox.Name = "mDebugBox";
        this.mDebugBox.setSize(208, 20);
        //= 63;
        this.mDebugBox.setVisible(false);
        // 
        // label1
        // 
        this.label1.setFont(new java.awt.Font("Courier New", 0, labelFontSize)); // NOI18N
        this.label1.setLocation(24, 0);
        //this.label1.Name = "label1";
        this.label1.setSize(514, 23);
        // 64;
        this.label1.setText("0    1    2    3  4    5    6    7    8   9    A    B    C   D    E  F");
        // 
        // label2
        // 
        this.label2.setFont(new java.awt.Font("Courier New", 0, labelFontSize)); // NOI18N
        this.label2.setLocation(0, 24);
        //this.label2.Name = "label2";
        this.label2.setSize(20, 25);
        // 65;
        this.label2.setText("0");
        // 
        // label3
        // 
        this.label3.setFont(new java.awt.Font("Courier New", 0, labelFontSize)); // NOI18N
        this.label3.setLocation(0, 56);
        //this.label3.Name = "label3";
        this.label3.setSize(20, 25);
        // 66;
        this.label3.setText("1");
        // 
        // label4
        // 
        this.label4.setFont(new java.awt.Font("Courier New", 0, labelFontSize)); // NOI18N
        this.label4.setLocation(0, 88);
        //this.label4.Name = "label4";
        this.label4.setSize(20, 25);
        // 67;
        this.label4.setText("2");
        // 
        // label5
        // 
        this.label5.setFont(new java.awt.Font("Courier New", 0, labelFontSize)); // NOI18N
        this.label5.setLocation(0, 120);
        //this.label5.Name = "label5";
        this.label5.setSize(20, 25);
        // 68;
        this.label5.setText("3");
        // 
        // ColorForm
        // 
        setPreferredSize(new java.awt.Dimension(528, 194));
        getContentPane().add(this.label5);
        getContentPane().add(this.label4);
        getContentPane().add(this.label3);
        getContentPane().add(this.label2);
        getContentPane().add(this.label1);
        getContentPane().add(this.mDebugBox);
        getContentPane().add(this.mCurrentColorTextBox);
        getContentPane().add(this.mCancelButton);
        getContentPane().add(this.mOKButton);
        getContentPane().add(this.panel0);
        getContentPane().add(this.panel1);
        getContentPane().add(this.panel2);
        getContentPane().add(this.panel3);
        getContentPane().add(this.panel5);
        getContentPane().add(this.panel6);
        getContentPane().add(this.panel7);
        getContentPane().add(this.panel4);
        getContentPane().add(this.panel8);
        getContentPane().add(this.panel9);
        getContentPane().add(this.panel10);
        getContentPane().add(this.panel11);
        getContentPane().add(this.panel14);
        getContentPane().add(this.panel15);
        getContentPane().add(this.panel13);
        getContentPane().add(this.panel12);
        getContentPane().add(this.panel25);
        getContentPane().add(this.panel26);
        getContentPane().add(this.panel16);
        getContentPane().add(this.panel17);
        getContentPane().add(this.panel18);
        getContentPane().add(this.panel19);
        getContentPane().add(this.panel21);
        getContentPane().add(this.panel22);
        getContentPane().add(this.panel23);
        getContentPane().add(this.panel20);
        getContentPane().add(this.panel24);
        getContentPane().add(this.panel27);
        getContentPane().add(this.panel30);
        getContentPane().add(this.panel31);
        getContentPane().add(this.panel29);
        getContentPane().add(this.panel28);
        getContentPane().add(this.panel57);
        getContentPane().add(this.panel44);
        getContentPane().add(this.panel45);
        getContentPane().add(this.panel47);
        getContentPane().add(this.panel46);
        getContentPane().add(this.panel43);
        getContentPane().add(this.panel42);
        getContentPane().add(this.panel41);
        getContentPane().add(this.panel40);
        getContentPane().add(this.panel36);
        getContentPane().add(this.panel39);
        getContentPane().add(this.panel38);
        getContentPane().add(this.panel37);
        getContentPane().add(this.panel35);
        getContentPane().add(this.panel34);
        getContentPane().add(this.panel32);
        getContentPane().add(this.panel33);
        getContentPane().add(this.panel60);
        getContentPane().add(this.panel61);
        getContentPane().add(this.panel63);
        getContentPane().add(this.panel62);
        getContentPane().add(this.panel59);
        getContentPane().add(this.panel56);
        getContentPane().add(this.panel52);
        getContentPane().add(this.panel55);
        getContentPane().add(this.panel54);
        getContentPane().add(this.panel53);
        getContentPane().add(this.panel51);
        getContentPane().add(this.panel50);
        getContentPane().add(this.panel49);
        getContentPane().add(this.panel48);
        getContentPane().add(this.panel58);
        getContentPane().add(this.mCurrentColorPanel);
        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        setMaximumSize(new java.awt.Dimension(550, 240));
        setPreferredSize(new java.awt.Dimension(545, 233));
        setMinimumSize(new java.awt.Dimension(544, 232));
        setSize(new java.awt.Dimension(544, 232));
        setName("ColorForm2");
        this.setTitle("Choose Color");
        pack();

    }// </editor-fold> 

    private void colorPanelClicked(java.awt.event.MouseEvent evt) {
        JLabel p = (JLabel) evt.getSource();
        if (p != null) {
            setCurrentColor(p.getBackground());
            setCurrentColorString(GetColorString(p));
            //mDebugBox.setText = String.format(
            //    "name=%s  r=%sg=%sb=%s"
            //    ,p.getName(), p.getBackground().getRed(),p.getBackground().getGreen(),p.getBackground().getBlue());
        }
    }

    private void mCancelButtonActionPerformed(java.awt.event.ActionEvent evt) {
        mCanceled = true;
        setVisible(false);
    }

    private void mOKButtonActionPerformed(java.awt.event.ActionEvent evt) {
        mCanceled = false;
        setVisible(false);
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
			/* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(ColorForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(ColorForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(ColorForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(ColorForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the dialog */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                ColorForm dialog = new ColorForm(new javax.swing.JFrame(), true);
                dialog.addWindowListener(new java.awt.event.WindowAdapter() {
                    @Override
                    public void windowClosing(java.awt.event.WindowEvent e) {
                        System.exit(0);
                    }
                });
                dialog.setVisible(true);
            }
        });
    }
    private javax.swing.JLabel panel1;
    private javax.swing.JLabel panel2;
    private javax.swing.JLabel panel3;
    private javax.swing.JLabel panel4;
    private javax.swing.JLabel panel5;
    private javax.swing.JLabel panel6;
    private javax.swing.JLabel panel7;
    private javax.swing.JLabel panel8;
    private javax.swing.JLabel panel9;
    private javax.swing.JLabel panel10;
    private javax.swing.JLabel panel11;
    private javax.swing.JLabel panel12;
    private javax.swing.JLabel panel13;
    private javax.swing.JLabel panel14;
    private javax.swing.JLabel panel15;
    private javax.swing.JLabel panel16;
    private javax.swing.JLabel panel17;
    private javax.swing.JLabel panel18;
    private javax.swing.JLabel panel19;
    private javax.swing.JLabel panel24;
    private javax.swing.JLabel panel25;
    private javax.swing.JLabel panel26;
    private javax.swing.JLabel panel27;
    private javax.swing.JLabel panel32;
    private javax.swing.JLabel panel33;
    private javax.swing.JLabel panel34;
    private javax.swing.JLabel panel35;
    private javax.swing.JLabel panel40;
    private javax.swing.JLabel panel41;
    private javax.swing.JLabel panel42;
    private javax.swing.JLabel panel43;
    private javax.swing.JLabel panel59;
    private javax.swing.JLabel panel0;
    private javax.swing.JLabel panel21;
    private javax.swing.JLabel panel22;
    private javax.swing.JLabel panel23;
    private javax.swing.JLabel panel20;
    private javax.swing.JLabel panel30;
    private javax.swing.JLabel panel31;
    private javax.swing.JLabel panel29;
    private javax.swing.JLabel panel28;
    private javax.swing.JLabel panel37;
    private javax.swing.JLabel panel38;
    private javax.swing.JLabel panel39;
    private javax.swing.JLabel panel36;
    private javax.swing.JLabel panel46;
    private javax.swing.JLabel panel47;
    private javax.swing.JLabel panel45;
    private javax.swing.JLabel panel44;
    private javax.swing.JLabel panel57;
    private javax.swing.JLabel panel58;
    private javax.swing.JLabel panel48;
    private javax.swing.JLabel panel49;
    private javax.swing.JLabel panel50;
    private javax.swing.JLabel panel51;
    private javax.swing.JLabel panel53;
    private javax.swing.JLabel panel54;
    private javax.swing.JLabel panel55;
    private javax.swing.JLabel panel52;
    private javax.swing.JLabel panel56;
    private javax.swing.JLabel panel62;
    private javax.swing.JLabel panel63;
    private javax.swing.JLabel panel61;
    private javax.swing.JLabel panel60;
    private javax.swing.JButton mOKButton;
    private javax.swing.JButton mCancelButton;
    private javax.swing.JTextField mCurrentColorTextBox;
    private javax.swing.JLabel mCurrentColorPanel;
}
